Web API Project using .Net Core 8

The goal of this project is to create an API which accepts CSV files and store them into a database.

- EF Core Migration Commands -

  • Add Migration: dotnet ef migrations add InitialMigration --project src/External/Infrastructure --startup-project src/Web/WebAPI --output-dir Data/Migrations --context ApplicationDbContext

   • Update Migration: dotnet ef database update --project src/External/Infrastructure --startup-project src/Web/WebAPI --context ApplicationDbContext

To seed Payment Methods table, execute InsertPaymentMethods in PizzaAnnualSalesAPI/SQL

- API Endpoints -

Note: Execute imports in order. Or make sure the data you're importing has its dependencies stored in the database already.

  • api/pizzatype/import-csv
  
  • api/pizza/import-csv
  
  • api/order/import-csv
  
  • api/orderdetail/import-csv
