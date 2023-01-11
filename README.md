# Paylocity Demo API
## Coding Challenge

Finalized 01/10/2023
Author: Sebastian Stefaniuk
Email:  sebboness@gmail.com

### Develop
- Create the SQL tables by running the statements in the `db.sql` file.
- Optionally add some dummy data by running the statements in the `db-data.sql` file.
- Create an environment variable named `PAYLOCITY_DEMO_DB` and save the DB connection string in it.
- Open Visual Studio, restore nuget packages, build, and run

### Run
Index page should open a Swagger UI to explore the API

### Notes
- Created this API with .Net Core 3.1 since that was my latest installed version
- Added all API endpoint placeholders that I could think of for this challenge
  - But most endpoints are not implemented in code
- Implemented endpoints:
  - Drafting a payroll for an employee
  - Getting a payroll draft
  - Getting all payroll drafts for an organization
  - Getting all benefit types for an organization
  - Getting all discount types for an organization
- No Authentication
- Hardcoded a 25% standard tax rate for all payrolls for fun