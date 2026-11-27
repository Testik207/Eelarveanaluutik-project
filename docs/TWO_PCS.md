# Working on two PCs

The school PC is cleared daily. Code is kept in GitHub and application data will be kept in a managed PostgreSQL database outside both development PCs.

This is the intended workflow. Exact startup commands will be added and checked when the application scaffold is ready. A cloud database has not been provisioned yet.

## Before starting on a PC

1. Check that Git, the required .NET SDK, Node.js and npm are available.
2. Clone the repository if the local folder was deleted.
3. Switch to the issue branch. If the folder already exists, fetch the latest changes and update the branch without discarding local work.
4. Restore .NET packages and React dependencies using the committed manifests and lockfiles.
5. Restore the API's private database connection string and local authentication settings from a private source. These settings are not stored in GitHub and may need to be entered again after school cleanup.
6. Start the API, React frontend and, when needed, the Worker.

## Database rules

- Only the API connects to PostgreSQL. React, MAUI and the Worker call the API.
- Both PCs use the same configured development database if they need to see the same records.
- Apply pending EF Core migrations deliberately. Normal startup must not drop the database or replace existing user data.
- Seed only synthetic example data and make seeding safe to repeat.
- The API must be able to reach the database from the school network. Check this before relying on the shared setup.
- Keep passwords, connection strings with credentials, tokens and private CSV files out of GitHub.

## Before leaving school

1. Save and check the current work.
2. Commit the completed changes with a clear message.
3. Push the branch to GitHub. A local commit alone is lost if the school folder is deleted before pushing.
4. Confirm that the latest commit appears on GitHub.

Pending work stays open in its issue. The next PC should continue from the pushed branch.

## Recovery check

After implementation, test a fresh checkout in a separate folder. Restore dependencies and private configuration, then confirm that previously saved transactions are still visible through the API. Record the actual commands and results here.

Cloud storage protects the application data from deletion of the local school folder. It does not restore unpushed code, local settings or locally stored files.
