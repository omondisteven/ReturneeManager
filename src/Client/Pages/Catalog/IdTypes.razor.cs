using ReturneeManager.Application.Features.IdTypes.Queries.GetAll;
using ReturneeManager.Client.Extensions;
using ReturneeManager.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ReturneeManager.Application.Features.IdTypes.Commands.AddEdit;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.IdType;
using ReturneeManager.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;

namespace ReturneeManager.Client.Pages.Catalog
{
    public partial class IdTypes
    {
        [Inject] private IIdTypeManager IdTypeManager { get; set; }

        [CascadingParameter] private HubConnection HubConnection { get; set; }

        private List<GetAllIdTypesResponse> _idTypeList = new();
        private GetAllIdTypesResponse _idType = new();
        private string _searchString = "";
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateIdTypes;
        private bool _canEditIdTypes;
        private bool _canDeleteIdTypes;
        private bool _canExportIdTypes;
        private bool _canSearchIdTypes;
        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateIdTypes = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.IdTypes.Create)).Succeeded;
            _canEditIdTypes = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.IdTypes.Edit)).Succeeded;
            _canDeleteIdTypes = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.IdTypes.Delete)).Succeeded;
            _canExportIdTypes = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.IdTypes.Export)).Succeeded;
            _canSearchIdTypes = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.IdTypes.Search)).Succeeded;

            await GetIdTypesAsync();
            _loaded = true;
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }

        private async Task GetIdTypesAsync()
        {
            var response = await IdTypeManager.GetAllAsync();
            if (response.Succeeded)
            {
                _idTypeList = response.Data.ToList();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
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
                var response = await IdTypeManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    await Reset();
                    await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    await Reset();
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }

        private async Task ExportToExcel()
        {
            var response = await IdTypeManager.ExportToExcelAsync(_searchString);
            if (response.Succeeded)
            {
                await _jsRuntime.InvokeVoidAsync("Download", new
                {
                    ByteArray = response.Data,
                    FileName = $"{nameof(IdTypes).ToLower()}_{DateTime.Now:ddMMyyyyHHmmss}.xlsx",
                    MimeType = ApplicationConstants.MimeTypes.OpenXml
                });
                _snackBar.Add(string.IsNullOrWhiteSpace(_searchString)
                    ? _localizer["IdTypes exported"]
                    : _localizer["Filtered IdTypes exported"], Severity.Success);
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
                _idType = _idTypeList.FirstOrDefault(c => c.Id == id);
                if (_idType != null)
                {
                    parameters.Add(nameof(AddEditIdTypeModal.AddEditIdTypeModel), new AddEditIdTypeCommand
                    {
                        Id = _idType.Id,
                        Name = _idType.Name,
                        Description = _idType.Description,
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditIdTypeModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await Reset();
            }
        }

        private async Task Reset()
        {
            _idType = new GetAllIdTypesResponse();
            await GetIdTypesAsync();
        }

        private bool Search(GetAllIdTypesResponse idType)
        {
            if (string.IsNullOrWhiteSpace(_searchString)) return true;
            if (idType.Name?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            if (idType.Description?.Contains(_searchString, StringComparison.OrdinalIgnoreCase) == true)
            {
                return true;
            }
            return false;
        }
    }
}