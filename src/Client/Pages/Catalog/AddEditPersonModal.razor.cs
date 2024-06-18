//AddEditPersonModal.razor.cs
using ReturneeManager.Application.Features.Brands.Queries.GetAll;
using ReturneeManager.Application.Features.IdTypes.Queries.GetAll;
using ReturneeManager.Application.Features.Genders.Queries.GetAll;
using ReturneeManager.Application.Features.Districts.Queries.GetAll;
using ReturneeManager.Application.Features.Divisions.Queries.GetAll;
using ReturneeManager.Application.Features.FromCountries.Queries.GetAll;
using ReturneeManager.Application.Features.Upazilas.Queries.GetAll;
using ReturneeManager.Application.Features.Wards.Queries.GetAll;
using ReturneeManager.Application.Features.Persons.Commands.AddEdit;
using ReturneeManager.Application.Requests;
using ReturneeManager.Client.Extensions;
using ReturneeManager.Shared.Constants.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blazored.FluentValidation;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.Brand;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.IdType;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.Gender;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.District;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.Division;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.FromCountry;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.Upazila;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.Ward;
using ReturneeManager.Client.Infrastructure.Managers.Catalog.Person;

namespace ReturneeManager.Client.Pages.Catalog
{
    public partial class AddEditPersonModal
    {
        [Inject] private IPersonManager PersonManager { get; set; }
        [Inject] private IIdTypeManager IdTypeManager { get; set; }
        [Inject] private IGenderManager GenderManager { get; set; }
        [Inject] private IDistrictManager DistrictManager { get; set; }
        [Inject] private IDivisionManager DivisionManager { get; set; }
        [Inject] private IUpazilaManager UpazilaManager { get; set; }
        [Inject] private IWardManager WardManager { get; set; }
        [Inject] private IFromCountryManager FromCountryManager { get; set; }

        [Parameter] public AddEditPersonCommand AddEditPersonModel { get; set; } = new();
        [CascadingParameter] private HubConnection HubConnection { get; set; }
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool IsIdNumberDisabled => AddEditPersonModel.IdTypeId == 0;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });
        private List<GetAllIdTypesResponse> _idTypes = new();
        private List<GetAllGendersResponse> _genders = new();
        private List<GetAllDistrictsResponse> _districts = new();
        private List<GetAllDivisionsResponse> _divisions = new();
        private List<GetAllUpazilasResponse> _upazilas = new();
        private List<GetAllWardsResponse> _wards = new();
        private List<GetAllFromCountriesResponse> _fromCountries = new();          

        

        private bool _isSameAsPresentAddress;
    private bool IsSameAsPresentAddress
    {
        get => _isSameAsPresentAddress;
        set
        {
            if (_isSameAsPresentAddress != value)
            {
                _isSameAsPresentAddress = value;
                if (_isSameAsPresentAddress)
                    {
                        AddEditPersonModel.HouseVillage2 = AddEditPersonModel.HouseVillage;
                        AddEditPersonModel.StreetAddress2 = AddEditPersonModel.StreetAddress;
                        AddEditPersonModel.DivisionId2 = AddEditPersonModel.DivisionId;
                        AddEditPersonModel.DistrictId2 = AddEditPersonModel.DistrictId;
                        AddEditPersonModel.UpazilaId2 = AddEditPersonModel.UpazilaId;
                        AddEditPersonModel.WardId2 = AddEditPersonModel.WardId;
                        AddEditPersonModel.PostCode2 = AddEditPersonModel.PostCode;
                    }
                    //else
                    //{
                    //    AddEditPersonModel.HouseVillage2 = "";
                    //    AddEditPersonModel.StreetAddress2 = "";
                    //    AddEditPersonModel.DivisionId2 = 0;
                    //    AddEditPersonModel.DistrictId2 = 0;
                    //    AddEditPersonModel.UpazilaId2 = 0;
                    //    AddEditPersonModel.WardId2 = 0;
                    //    AddEditPersonModel.PostCode2 = "";
                    //}
                }
            }
        }

        public void Cancel()
            {
                MudDialog.Cancel();
            }

        private async Task SaveAsync()
        {
            var response = await PersonManager.SaveAsync(AddEditPersonModel);
            if (response.Succeeded)
            {
                _snackBar.Add(response.Messages[0], Severity.Success);
                await HubConnection.SendAsync(ApplicationConstants.SignalR.SendUpdateDashboard);
                MudDialog.Close();
            }
            else
            {
                foreach (var message in response.Messages)
                {
                    _snackBar.Add(message, Severity.Error);
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {          

            await LoadDataAsync();
            HubConnection = HubConnection.TryInitialize(_navigationManager);
            if (HubConnection.State == HubConnectionState.Disconnected)
            {
                await HubConnection.StartAsync();
            }
        }       

        private async Task LoadDataAsync()
        {
            await LoadImageAsync();
            await LoadIdTypesAsync();
            await LoadGendersAsync();
            await LoadDistrictsAsync();
            await LoadDivisionsAsync();
            await LoadUpazilasAsync();
            await LoadWardsAsync();
            await LoadFromCountriesAsync();
        }
        

        private async Task LoadIdTypesAsync()
        {
            var data = await IdTypeManager.GetAllAsync();
            if (data.Succeeded)
            {
                _idTypes = data.Data;
            }
        }

        private async Task LoadGendersAsync()
        {
            var data = await GenderManager.GetAllAsync();
            if (data.Succeeded)
            {
                _genders = data.Data;
            }
        }

        private async Task LoadDistrictsAsync()
        {
            var data = await DistrictManager.GetAllAsync();
            if (data.Succeeded)
            {
                _districts = data.Data;
            }
        }

        private async Task LoadDivisionsAsync()
        {
            var data = await DivisionManager.GetAllAsync();
            if (data.Succeeded)
            {
                _divisions = data.Data;
            }
        }

        private async Task LoadUpazilasAsync()
        {
            var data = await UpazilaManager.GetAllAsync();
            if (data.Succeeded)
            {
                _upazilas = data.Data;
            }
        }

        private async Task LoadWardsAsync()
        {
            var data = await WardManager.GetAllAsync();
            if (data.Succeeded)
            {
                _wards = data.Data;
            }
        }

        private async Task LoadFromCountriesAsync()
        {
            var data = await FromCountryManager.GetAllAsync();
            if (data.Succeeded)
            {
                _fromCountries = data.Data;
            }
        }

        private async Task LoadImageAsync()
        {
            var data = await PersonManager.GetPersonImageAsync(AddEditPersonModel.Id);
            if (data.Succeeded)
            {
                var imageData = data.Data;
                if (!string.IsNullOrEmpty(imageData))
                {
                    AddEditPersonModel.ImageDataURL = imageData;
                }
            }
        }

        private void DeleteAsync()
        {
            AddEditPersonModel.ImageDataURL = null;
            AddEditPersonModel.UploadRequest = new UploadRequest();
        }

        private IBrowserFile _file;

        private async Task UploadFiles(InputFileChangeEventArgs e)
        {
            _file = e.File;
            if (_file != null)
            {
                var extension = Path.GetExtension(_file.Name);
                var format = "image/png";
                var imageFile = await e.File.RequestImageFileAsync(format, 400, 400);
                var buffer = new byte[imageFile.Size];
                await imageFile.OpenReadStream().ReadAsync(buffer);
                AddEditPersonModel.ImageDataURL = $"data:{format};base64,{Convert.ToBase64String(buffer)}";
                AddEditPersonModel.UploadRequest = new UploadRequest { Data = buffer, UploadType = Application.Enums.UploadType.Person, Extension = extension };
            }
        }        

        private async Task<IEnumerable<int>> SearchIdTypes(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _idTypes.Select(x => x.Id);

            return _idTypes.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }

        private async Task<IEnumerable<int>> SearchGenders(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _genders.Select(x => x.Id);

            return _genders.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }

        private async Task<IEnumerable<int>> SearchDistricts(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _districts.Select(x => x.Id);

            return _districts.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }

        private async Task<IEnumerable<int>> SearchDivisions(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _divisions.Select(x => x.Id);

            return _divisions.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }

        private async Task<IEnumerable<int>> SearchUpazilas(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _upazilas.Select(x => x.Id);

            return _upazilas.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }

        private async Task<IEnumerable<int>> SearchWards(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _wards.Select(x => x.Id);

            return _wards.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }

        private async Task<IEnumerable<int>> SearchFromCountries(string value)
        {
            // In real life use an asynchronous function for fetching data from an api.
            await Task.Delay(5);

            // if text is null or empty, show complete list
            if (string.IsNullOrEmpty(value))
                return _fromCountries.Select(x => x.Id);

            return _fromCountries.Where(x => x.Name.Contains(value, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.Id);
        }
    }
}