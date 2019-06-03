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
        }
    }
}
