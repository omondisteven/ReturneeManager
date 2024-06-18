using ReturneeManager.Application.Interfaces.Services;
using ReturneeManager.Infrastructure.Contexts;
using ReturneeManager.Infrastructure.Helpers;
using ReturneeManager.Infrastructure.Models.Identity;
using ReturneeManager.Shared.Constants.Permission;
using ReturneeManager.Shared.Constants.Role;
using ReturneeManager.Shared.Constants.User;
using ReturneeManager.Domain.Entities.Catalog;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReturneeManager.Infrastructure
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly IStringLocalizer<DatabaseSeeder> _localizer;
        private readonly BlazorHeroContext _db;
        private readonly UserManager<BlazorHeroUser> _userManager;
        private readonly RoleManager<BlazorHeroRole> _roleManager;

        public DatabaseSeeder(
            UserManager<BlazorHeroUser> userManager,
            RoleManager<BlazorHeroRole> roleManager,
            BlazorHeroContext db,
            ILogger<DatabaseSeeder> logger,
            IStringLocalizer<DatabaseSeeder> localizer)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _logger = logger;
            _localizer = localizer;
        }

        public void Initialize()
        {
            AddAdministrator();
            AddBasicUser();
            SeedIdTypes();
            SeedDistricts();
            SeedDivisions();
            SeedUpazilas();
            SeedFromCountries();
            SeedWards();
            _db.SaveChanges();
        }

        private void AddAdministrator()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var adminRole = new BlazorHeroRole(RoleConstants.AdministratorRole, _localizer["Administrator role with full permissions"]);
                var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                if (adminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(adminRole);
                    adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                    _logger.LogInformation(_localizer["Seeded Administrator Role."]);
                }
                //Check if User Exists
                var superUser = new BlazorHeroUser
                {
                    FirstName = "Mukesh",
                    LastName = "Murugan",
                    Email = "mukesh@blazorhero.com",
                    UserName = "mukesh",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation(_localizer["Seeded Default SuperAdmin User."]);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
                foreach (var permission in Permissions.GetRegisteredPermissions())
                {
                    await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
                }
            }).GetAwaiter().GetResult();
        }

        private void AddBasicUser()
        {
            Task.Run(async () =>
            {
                //Check if Role Exists
                var basicRole = new BlazorHeroRole(RoleConstants.BasicRole, _localizer["Basic role with default permissions"]);
                var basicRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.BasicRole);
                if (basicRoleInDb == null)
                {
                    await _roleManager.CreateAsync(basicRole);
                    _logger.LogInformation(_localizer["Seeded Basic Role."]);
                }
                //Check if User Exists
                var basicUser = new BlazorHeroUser
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john@blazorhero.com",
                    UserName = "johndoe",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                    IsActive = true
                };
                var basicUserInDb = await _userManager.FindByEmailAsync(basicUser.Email);
                if (basicUserInDb == null)
                {
                    await _userManager.CreateAsync(basicUser, UserConstants.DefaultPassword);
                    await _userManager.AddToRoleAsync(basicUser, RoleConstants.BasicRole);
                    _logger.LogInformation(_localizer["Seeded User with Basic Role."]);
                }
            }).GetAwaiter().GetResult();
        }

        private void SeedIdTypes()
        {
            if (!_db.IdTypes.Any())
            {
                _db.IdTypes.AddRange(
                    new IdType { Name = "Passport", Description = "Passport" },
                    new IdType { Name = "Birth Certificate", Description = "Birth Certificate" },
                    new IdType { Name = "National ID", Description = "National ID" }
                );
                _logger.LogInformation(_localizer["Seeded IdTypes."]);
            }
        }

        private void SeedDistricts()
        {
            if (!_db.Districts.Any())
            {
                _db.Districts.AddRange(
                    new District { Name = "BAGERHAT", Description = "BAGERHAT" },
                    new District { Name = "BANDARBAN", Description = "BANDARBAN" },
                    new District { Name = "BARGUNA", Description = "BARGUNA" }
                );
                _logger.LogInformation(_localizer["Seeded Districts."]);
            }
        }

        private void SeedDivisions()
        {
            if (!_db.Divisions.Any())
            {
                _db.Divisions.AddRange(
                    new Division { Name = "BARISAL", Description = "BARISAL" },
                    new Division { Name = "CHITAGONG", Description = "CHITAGONG" },
                    new Division { Name = "DHAKA", Description = "DHAKA" }
                );
                _logger.LogInformation(_localizer["Seeded Divisions."]);
            }
        }

        private void SeedUpazilas()
        {
            if (!_db.Upazilas.Any())
            {
                _db.Upazilas.AddRange(
                    new Upazila { Name = "ABHAYNAGAR", Description = "ABHAYNAGAR" },
                    new Upazila { Name = "ADAMDIGHI", Description = "ADAMDIGHI" },
                    new Upazila { Name = "ADARSHA SADAR", Description = "ADARSHA SADAR" }
                );
                _logger.LogInformation(_localizer["Seeded Upazilas."]);
            }
        }

        private void SeedFromCountries()
        {
            if (!_db.FromCountries.Any())
            {
                _db.FromCountries.AddRange(
                    new FromCountry { Name = "Afghanistan", Description = "Afghanistan 1" },
                    new FromCountry { Name = "Pakistan", Description = "Pakistan" },
                    new FromCountry { Name = "Iran", Description = "Iran" }
                );
                _logger.LogInformation(_localizer["Seeded FromCountries."]);
            }
        }

        private void SeedWards()
        {
            if (!_db.Wards.Any())
            {
                _db.Wards.AddRange(
                    new Ward { Name = "BAGHUTIA", Description = "BAGHUTIA" },
                    new Ward { Name = "CHALISHA", Description = "CHALISHA" },
                    new Ward { Name = "PAYRA", Description = "PAYRA" }
                );
                _logger.LogInformation(_localizer["Seeded Wards."]);
            }
        }
    }
}
