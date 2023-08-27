## REZ Development Report (INF-0999)
Provided this is the Final Project of the training course in Microsoft technologies at the Institute of Computing at the State University of Campinas (UNICAMP), it is important that the students follow some base criteria about each subject taught during the course. For that reason, the following topics were considered during development:

- C# Programming Language and known software architecture patterns;
- Code versioning;
- Hosting;
- Project Management;
- User Experience;
- User Interface Programming;
- Safe Programming;

### Programming and Architecture Pattern
### Code Versioning
For code versioning we used the Git + GitHub remote repository workflow. Branch names are specific for a feature (or set of features) that the developers are currently working, so Pull Requests can be concise and easy to read. They can also be attached to an specific issue so both are closed after a merge.

For this project we have been using the [Shared Repository Model](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/getting-started/about-collaborative-development-models#shared-repository-model), in which all collaborators are granted access to the single shared repository. Each branch is created to meet an specific issue requirements so we can merge the code via a PR with code review.

Branch names can be specified using a simple pattern like:
```
add_functional_shoppingcart
fix_menu_images
doc_devreport
```

The commit message standards we are using may follow (but not only) these rules:
```
* feat – a new feature is introduced with the changes
* fix – a bug fix has occurred
* chore – changes that do not relate to a fix or feature and don't modify src or test files (for example updating dependencies)
* refactor – refactored code that neither fixes a bug nor adds a feature
* docs – updates to documentation such as a the README or other markdown files
* style – changes that do not affect the meaning of the code, likely related to code formatting such as white-space, missing semi-colons, and so on.
* test – including new or correcting previous tests
* perf – performance improvements
* ci – continuous integration related
* build – changes that affect the build system or external dependencies
* revert – reverts a previous commit
```
On the GitHub repository, we have been opening and tracking issues for each feature or bugfix. The issues have a simple template for messages, in which a `Description` and `Further Information` should be provided. Issues can be written in English or Portuguese.

![Alt text](./assets/issue-example-1.png)

- [GitHub Repository](https://github.com/lknknm/REZ-menu-app);
- You can see Open and Closed Issues [here](https://github.com/lknknm/REZ-menu-app/issues);
- You can see the Open and Merged Pull Requests with discussion [here](https://github.com/lknknm/REZ-menu-app/pulls).

### Project Management
We managed the project with GitHub Projects by applying the `Agile/Scrum` methodology. This way we can track issues, assign them to each member of the project group and also track the development status. 

The `Agile/Scrum` methodology was used so the group could discuss the current open issues, understand each problem together and organize new sprints to solve them. Features or bugfixes could be suggested by any member of the group (in an ideal case, we could also include users feedback), as long as they provide clear information about the problems they are encountering during the development process.

After tracking each issue and organizing them by `Priority`, we add them to the Backlog, assign them to a group member to solve, and track the status during the Sprint. 

![Alt text](./assets/issue-example-2.png)

![Alt text](./assets/scrumproject.png)

- You can see the GitHub Projects page with the current development status [here](https://github.com/users/lknknm/projects/1/views/1).
- You can see Open and Closed Issues [here](https://github.com/lknknm/REZ-menu-app/issues);
- You can see the Open and Merged Pull Requests with discussion [here](https://github.com/lknknm/REZ-menu-app/pulls).

### Hosting
### User Experience
### User Interface Programming
### Safe Programming