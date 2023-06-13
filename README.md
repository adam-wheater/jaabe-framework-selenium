# Selenium Framework Project

This project provides a robust and flexible testing framework using Selenium WebDriver for automating web applications. It includes utility classes that make it easier to interact with web elements, handle multiple browsers, and perform common testing tasks.

## Structure

The project is organized as follows:

- **Helper.cs**: This class contains helper functions for common tasks such as finding elements, waiting for elements, and handling exceptions. It uses log4net for logging activities and exceptions which makes debugging easier.

## Adding New Browsers

The current implementation supports Firefox, Chrome, and Internet Explorer. To add more browsers, follow the steps below:

1. Download the WebDriver executable for the new browser.
2. Update the `SeleniumTestBase.cs` to include a new case in the switch statement for the new browser. You will need to add code to initialize the WebDriver for the new browser similar to the existing cases.
3. Add the necessary WebDriver setup in the setup method of your test classes.

## Usage

To run the tests, simply clone the repository, navigate to the project directory, and run your tests using your preferred test runner.

## Contributing

Contributions are welcome! Please read the contributing guidelines before making any changes.

## License

This project is licensed under the MIT License. See the LICENSE file for details.
