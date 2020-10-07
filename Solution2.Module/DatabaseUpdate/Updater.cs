﻿using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Solution2.Module.BusinessObjects;
using Bogus;

namespace Solution2.Module.DatabaseUpdate {
    // For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            //string name = "MyName";
            //DomainObject1 theObject = ObjectSpace.FindObject<DomainObject1>(CriteriaOperator.Parse("Name=?", name));
            //if(theObject == null) {
            //    theObject = ObjectSpace.CreateObject<DomainObject1>();
            //    theObject.Name = name;
            //}
            PermissionPolicyUser sampleUser = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "User"));
            if(sampleUser == null) {
                sampleUser = ObjectSpace.CreateObject<PermissionPolicyUser>();
                sampleUser.UserName = "User";
                sampleUser.SetPassword("");
            }
            PermissionPolicyRole defaultRole = CreateDefaultRole();
            sampleUser.Roles.Add(defaultRole);

            PermissionPolicyUser userAdmin = ObjectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", "Admin"));
            if(userAdmin == null) {
                userAdmin = ObjectSpace.CreateObject<PermissionPolicyUser>();
                userAdmin.UserName = "Admin";
                // Set a password if the standard authentication type is used
                userAdmin.SetPassword("");
            }
			// If a role with the Administrators name doesn't exist in the database, create this role
            PermissionPolicyRole adminRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Administrators"));
            if(adminRole == null) {
                adminRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                adminRole.Name = "Administrators";
            }
            adminRole.IsAdministrative = true;
			userAdmin.Roles.Add(adminRole);


            DodajStawke("23%", 23m);
            DodajStawke("07%", 7m);
            DodajStawke("03%", 3m);
            DodajStawke("0%", 0m);
            DodajStawke("ZW%", 0m);
            DodajStawke("NP%", 0m);
            ObjectSpace.CommitChanges();

            var stawki = ObjectSpace.GetObjectsQuery<StawkaVat>().ToList();

            var bogusUsluga = new Faker<Usluga>("pl")
                .CustomInstantiator(f=> ObjectSpace.CreateObject<Usluga>())
                .RuleFor(o=>o.Nazwa , f=> f.Commerce.ProductName())
                .RuleFor(o => o.CenaJednostkowa, f => f.Random.Decimal(0.01m, 1000m))
                .RuleFor(o => o.StawkaVat, f => f.PickRandom(stawki));
            bogusUsluga.Generate(100);

            var bogusProdukt = new Faker<Produkt>("pl")
    .CustomInstantiator(f => ObjectSpace.CreateObject<Produkt>())
    .RuleFor(o => o.Nazwa, f => f.Commerce.ProductName())
    .RuleFor(o => o.CenaJednostkowa, f => f.Random.Decimal(0.01m, 1000m))
    .RuleFor(o => o.StawkaVat, f => f.PickRandom(stawki));
            bogusProdukt.Generate(100);


            var bogusTowar = new Faker<Towar>("pl")
            .CustomInstantiator(f => ObjectSpace.CreateObject<Towar>())
            .RuleFor(o => o.Nazwa, f => f.Commerce.ProductName())
            .RuleFor(o => o.CenaJednostkowa, f => f.Random.Decimal(0.01m, 1000m))
            .RuleFor(o => o.StawkaVat, f => f.PickRandom(stawki));
            bogusTowar.Generate(100);

            ObjectSpace.CommitChanges(); //This line persists created object(s).
        }

        private void DodajStawke(string symbol, decimal wartosc)
        {
            var stawka = ObjectSpace.FindObject<StawkaVat>(CriteriaOperator.Parse("Symbol = ?", symbol));
            //  stawka = ObjectSpace.GetObjectByKey<StawkaVat>(symbol);
            if (stawka == null)
            {
                stawka = ObjectSpace.CreateObject<StawkaVat>();
                stawka.Symbol = symbol;
                stawka.Wartosc = wartosc;
            }
        }

        public override void UpdateDatabaseBeforeUpdateSchema() {
            base.UpdateDatabaseBeforeUpdateSchema();
            //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
            //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
            //}
        }
        private PermissionPolicyRole CreateDefaultRole() {
            PermissionPolicyRole defaultRole = ObjectSpace.FindObject<PermissionPolicyRole>(new BinaryOperator("Name", "Default"));
            if(defaultRole == null) {
                defaultRole = ObjectSpace.CreateObject<PermissionPolicyRole>();
                defaultRole.Name = "Default";

				defaultRole.AddObjectPermission<PermissionPolicyUser>(SecurityOperations.Read, "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddNavigationPermission(@"Application/NavigationItems/Items/Default/Items/MyDetails", SecurityPermissionState.Allow);
				defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "ChangePasswordOnFirstLogon", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
				defaultRole.AddMemberPermission<PermissionPolicyUser>(SecurityOperations.Write, "StoredPassword", "[Oid] = CurrentUserId()", SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<PermissionPolicyRole>(SecurityOperations.Read, SecurityPermissionState.Deny);
                defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.ReadWriteAccess, SecurityPermissionState.Allow);
				defaultRole.AddTypePermissionsRecursively<ModelDifference>(SecurityOperations.Create, SecurityPermissionState.Allow);
                defaultRole.AddTypePermissionsRecursively<ModelDifferenceAspect>(SecurityOperations.Create, SecurityPermissionState.Allow);
            }
            return defaultRole;
        }
    }
}
