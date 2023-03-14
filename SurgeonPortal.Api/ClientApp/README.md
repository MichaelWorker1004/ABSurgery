# Code Quality

Code quality tools have been added to this project to ensure a consistent coding style, to avoid common errors and to reduce friction for development.

## [Husky](https://typicode.github.io/husky/#/)

### What is Husky?

Modern native git hooks made easy

Husky manages [git hooks](https://git-scm.com/docs/githooks). This allows code quality checks before a commit.

### Why?

Each commit being processed first by a code style quick check, then by linting ensures consistent, quality code that is standard across all files.

## [Prettier](https://prettier.io)

### What is Prettier?

- An opinionated code formatter
- Supports many languages
- Integrates with most editors
- Has few options

### Why?

- Your code is formatted on save
- No need to discuss style in code review
- Saves you time and energy

### Prettier Settings

Prettier is opinionated which means there is very little you can configure about it.
It does, however defer to `.editorconfig` and does have some options that can be set.

#### Strings

Double or single quotes? Prettier chooses the one which results in the fewest number of escapes. "It's gettin' better!", not 'It\'s gettin\' better!'. In case of a tie or the string not containing any quotes, Prettier defaults to double quotes (but that can be changed via the singleQuote option).

JSX has its own option for quotes: jsxSingleQuote. JSX takes its roots from HTML, where the dominant use of quotes for attributes is double quotes. Browser developer tools also follow this convention by always displaying HTML with double quotes, even if the source code uses single quotes. A separate option allows using single quotes for JS and double quotes for "HTML" (JSX).

Prettier maintains the way your string is escaped. For example, "🙂" won’t be formatted into "\uD83D\uDE42" and vice versa.

## [ESLint](https://eslint.org)

### Why?

## Unit Tests

Linting also kicks off unit tests which must succeed. The unit tests use [Karma](https://www.npmjs.com/package/karma) and [Jasmine](https://jasmine.github.io)

# Libraries

## [LIT Element](https://lit.dev/docs/api/LitElement/)

## [Shoelace](https://shoelace.style)

## [NGXS](https://www.ngxs.io/getting-started/why)

![img](https://490253082-files.gitbook.io/~/files/v0/b/gitbook-x-prod.appspot.com/o/spaces%2F-L9CoGJCq3UCfKJ7RCUg-347405460%2Fuploads%2Fgit-blob-7371002ded66c4455ca986a4c8e7c1f6849ffef9%2Fdiagram.png?alt=media)

# About Project Setup

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 15.1.6.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory in the ClientApp root.

Run `npm run build` for regular production builds (this is the default for Angular)

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

To run Karma you may need to install Google Chrome and set the environment variable CHROME_BIN

## Convert module based projects to standalone

To convert a project from am Angular module based application to a standalone
successively run the command: `ng g @angular/core:standalone`
Pick each option in order they offered, ending with the bootstrap option. Generally
select the default path when asked.

This will convert a module based application to a standalone and remove
unused modules. You may need to do some manual cleanup.
