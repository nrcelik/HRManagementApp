namespace HRManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(nullable: false, maxLength: 200),
                        City_Id = c.Int(),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        Surname = c.String(nullable: false, maxLength: 15),
                        Department_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.Department_Id, cascadeDelete: true)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.EmployeesDetails",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        BirthDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        MartialStatus = c.Boolean(nullable: false),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Adress_Id = c.Int(),
                        Employees_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Addresses", t => t.Adress_Id)
                .ForeignKey("dbo.Employees", t => t.Employees_Id)
                .Index(t => t.Adress_Id)
                .Index(t => t.Employees_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 8),
                        Password = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        RememberMe = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeesDetails", "Employees_Id", "dbo.Employees");
            DropForeignKey("dbo.EmployeesDetails", "Adress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Employees", "Department_Id", "dbo.Departments");
            DropForeignKey("dbo.Addresses", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.Addresses", "City_Id", "dbo.Cities");
            DropIndex("dbo.EmployeesDetails", new[] { "Employees_Id" });
            DropIndex("dbo.EmployeesDetails", new[] { "Adress_Id" });
            DropIndex("dbo.Employees", new[] { "Department_Id" });
            DropIndex("dbo.Addresses", new[] { "Country_Id" });
            DropIndex("dbo.Addresses", new[] { "City_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.EmployeesDetails");
            DropTable("dbo.Employees");
            DropTable("dbo.Departments");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Addresses");
        }
    }
}
