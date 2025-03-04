## General informations
Service is based on CleanArchitecture template, with following assumptions:
- SqlLite database for keeping information about card ACTIONS configuration
- Standard user authentication, but CardCheck request set to anonymous mode, just for simplify testing
## Implementation details
- Registered dummy CardService which should communicate with HTTP service to retrieve card onwers data, suggested dummy logic put there
- GetCardAccessCommand created for handling logic of selection ACTIONS
- Suggested enums extended for optimize purpose
