﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Users;
using Application.Commands.Characters;
using EFCommands.Users;
using EFCommands.Characters;
using EFDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Application.Commands.GameClasses;
using EFCommands.GameClasses;
using Application.Commands.Genders;
using EFCommands.Genders;
using EFCommands.Races;
using Application.Commands.Races;
using EFCommands.Items;
using Application.Commands.Items;
using EFCommands.ItemTypes;
using Application.Commands.ItemTypes;
using Application.Commands.ItemQualities;
using EFCommands.ItemQualities;
using EFCommands.Roles;
using Application.Commands.Roles;
using Application.Commands.Orders;
using EFCommands.Orders;
using EFCommands.Authorization;
using Application.Commands.Authorization;
using Api.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Application.HelperClasses;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<ProjectContext>();
            //Users
            services.AddTransient<IGetUserCommand, EFGetUserCommand>();
            services.AddTransient<IGetUsersCommand, EFGetUsersCommand>();
            services.AddTransient<IEditUserCommand, EFEditUserCommand>();
            services.AddTransient<IAddUserCommand, EFAddUserCommand>();
            services.AddTransient<IDeleteUserCommand, EFDeleteUserCommand>();
            //Characters
            services.AddTransient<IGetCharacterCommand, EFGetCharacterCommand>();
            services.AddTransient<IGetCharactersCommand, EFGetCharactersCommand>();
            services.AddTransient<IDeleteCharacterCommand, EFDeleteCharacterCommand>();
            services.AddTransient<IAddCharacterCommand, EFAddCharacterCommand>();
            services.AddTransient<IEditCharacterCommand, EFEditCharacterCommand>();
            services.AddTransient<IGetCharacterInventoryCommand, EFGetCharacterInventoryCommand>();
            //GameClasses
            services.AddTransient<IGetGameClassCommand, EFGetGameClassCommand>();
            services.AddTransient<IGetGameClassesCommand, EFGetGameClassesCommand>();
            services.AddTransient<IAddGameClassCommand, EFAddGameClassCommandCommand>();
            services.AddTransient<IEditGameClassCommand, EFEditGameClassCommand>();
            services.AddTransient<IDeleteGameClassCommand, EFDeleteGameClassCommand>();
            //Genders
            services.AddTransient<IGetGendersCommand, EFGetGendersCommand>();
            services.AddTransient<IGetGenderCommand, EFGetGenderCommand>();
            services.AddTransient<IAddGenderCommand, EFAddGenderCommand>();
            services.AddTransient<IEditGenderCommand, EFEditGenderCommand>();
            services.AddTransient<IDeleteGenderCommand, EFDeleteGenderCommand>();
            //Races
            services.AddTransient<IGetRacesCommand, EFGetRacesCommand>();
            services.AddTransient<IGetRaceCommand, EFGetRaceCommand>();
            services.AddTransient<IAddRaceCommand, EFAddRaceCommand>();
            services.AddTransient<IEditRaceCommand, EFEditRaceCommand>();
            services.AddTransient<IDeleteRaceCommand, EFDeleteRaceCommand>();
            //Items
            services.AddTransient<IGetItemsCommand, EFGetItemsCommand>();
            services.AddTransient<IGetItemCommand, EFGetItemCommand>();
            services.AddTransient<IAddItemCommand, EFAddItemCommand>();
            services.AddTransient<IEditItemCommand, EFEditItemCommand>();
            services.AddTransient <IDeleteItemCommand, EFDeleteItemCommand>();
            //ItemTypes
            services.AddTransient<IGetItemTypesCommand, EFGetItemTypesCommand>();
            services.AddTransient<IGetItemTypeCommand, EFGetItemTypeCommand>();
            services.AddTransient<IAddItemTypeCommand, EFAddItemTypeCommand>();
            services.AddTransient<IEditItemTypeCommand, EFEditItemTypeCommand>();
            services.AddTransient<IDeleteItemTypeCommand, EFDeleteItemTypeCommand>();
            //ItemQualities
            services.AddTransient<IGetItemQualitiesCommand, EFGetItemQualitiesCommand>();
            services.AddTransient<IGetItemQualityCommand, EFGetItemQualityCommand>();
            services.AddTransient<IAddItemQualityCommand, EFAddItemQualityCommand>();
            services.AddTransient<IEditItemQualityCommand, EFEditItemQualityCommand>();
            services.AddTransient<IDeleteItemQualityCommand, EFDeleteItemQualityCommand>();
            //Roles
            services.AddTransient<IGetRolesCommand, EFGetRolesCommand>();
            services.AddTransient<IGetRoleCommand, EFGetRoleCommand>();
            //Orders
            services.AddTransient<IMakeOrderCommand, EFMakeOrderCommand>();
            //Auth
            services.AddTransient<IGetAuthUserCommand, EFGetAuthUserCommand>();
            //For Auth
            var key = Configuration.GetSection("Encryption")["key"];
            var enc = new Encryption(key);

            services.AddSingleton(enc);

            services.AddTransient(s =>
            {
                var http = s.GetRequiredService<IHttpContextAccessor>();
                var value = http.HttpContext.Request.Headers["Authorization"].ToString();
                var encryption = s.GetRequiredService<Encryption>();

                try
                {
                    var decodedString = encryption.DecryptString(value);
                    decodedString = decodedString.Replace("\f", "");
                    var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);
                    user.IsLogged = true;
                    return user;
                }
                catch (Exception)
                {
                    return new LoggedUser
                    {
                        IsLogged = false
                    };
                }
            });

            //Swagger(Swashbuckle)
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "GeneralGoodsShop API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

        }
    }
}
