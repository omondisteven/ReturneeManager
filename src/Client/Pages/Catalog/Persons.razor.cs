﻿using ReturneeManager.Application.Features.Persons.Queries.GetAllPaged;
using ReturneeManager.Application.Requests.Catalog;
using ReturneeManager.Client.Extensions;
using ReturneeManager.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.Persons.Commands.AddEdit;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.Person;
using ReturneeManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;

namespace ReturneeManager.Client.Pages.Catalog
{
    public partial class Persons
    {
        [Inject] private IPersonManager PersonManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private IEnumerable<GetAllPagedPersonsResponse> _pagedData;
        private MudTable<GetAllPagedPersonsResponse> _table;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreatePersons;
        private bool _canEditPersons;
        private bool _canDeletePersons;
        private bool _canExportPersons;
        private bool _canSearchPersons;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreatePersons = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Persons.Create)).Succeeded;
            _canEditPersons = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Persons.Edit)).Succeeded;
            _canDeletePersons = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Persons.Delete)).Succeeded;
            _canExportPersons = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Persons.Export)).Succeeded;
            _canSearchPersons = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Persons.Search)).Succeeded;

            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task<TableData<GetAllPagedPersonsResponse>> ServerReload(TableState state)
        {
            if (!string.IsNullOrWhiteSpace(_searchString))
            {
                state.Page = 0;
            }
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<GetAllPagedPersonsResponse> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            if (!string.IsNullOrEmpty(state.SortLabel))
            {
                orderings = state.SortDirection != SortDirection.None ? new[] {$"{state.SortLabel} {state.SortDirection}"} : new[] {$"{state.SortLabel}"};
            }

            var request = new GetAllPagedPersonsRequest { PageSize = pageSize, PageNumber = pageNumber + 1, SearchString = _searchString, Orderby = orderings };
            var response = await PersonManager.GetPersonsAsync(request);
            if (response.Succeeded)
            {
                _totalItems = response.TotalCount;
                _currentPage = response.CurrentPage;
                _pagedData = response.Data;
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            _table.ReloadServerData();
        }

        private async Task ExportToExcel()
        {
            var response = await PersonManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(Persons).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["Persons exported"]
                    : _localizer["Filtered Persons exported"], Severity.Success);
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var person = _pagedData.FirstOrDefault(c => c.Id == id);
                if (person != null)
                {
                    parameters.Add(nameof(AddEditPersonModal.AddEditPersonModel), new AddEditPersonCommand
                    {
                        Id = person.Id,
                        Name = person.Name,                        
                        IdTypeId = person.IdTypeId,
                        IdNumber = person.IdNumber,
                        GenderId = person.GenderId,
                        DistrictId = person.DistrictId,
                        DivisionId = person.DivisionId,
                        FromCountryId = person.FromCountryId,
                        UpazilaId = person.UpazilaId,
                        WardId = person.UpazilaId,
                        DateOfBirth = person.DateOfBirth,
                        HouseVillage = person.HouseVillage,
                        StreetAddress = person.StreetAddress,
                        MobileNumber = person.MobileNumber,
                        PostCode = person.PostCode,
                        FatherName = person.FatherName,
                        MotherName = person.MotherName,
                        ReturnReason = person.ReturnReason,
                        ReturnDate = person.ReturnDate,
                        ReturnDocument = person.ReturnDocument,
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditPersonModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                OnSearch("");
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = _localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await PersonManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    OnSearch("");
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    OnSearch("");
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }
    }
}