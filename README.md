# C# - Make a Recording Tutorial

This project serves as a guide to help you build an application with FreeClimb. Specifically, the project will:

- Creates an outgoing call to the `to` number
- Prompts the participant for a recording
- Records a message from the participant

## Setting up your new app within your FreeClimb account

To get started using a FreeClimb account, follow the instructions [here](https://persephony-docs.readme.io/docs/getting-started-with-persephony).

## Setting up the Tutorial

1. Install the nuget packages necessary using command:

   ```bash
   $ dotnet add package freeclimb-cs-sdk --version 1.0.0.1
   ```

2. Configure environment variables

   | ENV VARIABLE            | DESCRIPTION                                                                                                                                                                             |
   | ----------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
   | ACCOUNT_ID              | Account ID which can be found under [API Keys](https://www.persephony.com/dashboard/portal/account/authentication) in Dashboard                                                         |
   | AUTH_TOKEN              | Authentication Token which can be found under [API Keys](https://www.persephony.com/dashboard/portal/account/authentication) in Dashboard                                               |
   | APPLICATION_ID | Appliction IDs can be found under [Apps](https://www.persephony.com/dashboard/portal/applications) |
   | HOST | The url of where your app is being hosted (e.g. yourHostedApp.com)

 

3. Provide a value for the variables `to` and `from` in FreeClimbController.cs. The `to` number is any phone number you wish to call. This number must be [verified](https://docs.persephony.com/docs/using-your-trial-account#section-verifying-outbound-numbers). `from` is a FreeClimb number that makes the call ([Incoming Numbers](https://www.persephony.com/dashboard/portal/numbers)).

## Runnning the Tutorial

1. Run the application using command:

   ```bash
   $ dotnet run
   ```

