# omicron
Simple github trending repository system developed with .net core + vscode in ubuntu budgie 17.04. Repository for study purpose. The objective is to create a simple, but full, DDD application using C# in a Linux environment.

## New technologies used
- .NET Core
- Nancy

## What's the idea?
Create a MVC project that shows data about trending repositories. Data shown will be today trending repositories, trending repositories of week, ranking up and down for each repository, trending languages.

## How am I planning to do that?
Scrapping trending repositories from Github using [Trending Page](https://github.com/trending) and (Git Repos API)[https://developer.github.com/v3/repos/].
- To scrap trending page I'm using [Html Agility Pack](http://html-agility-pack.net/)
- To communicate with Git API I'm using [RestSharp](http://restsharp.org)
I will scrap daily data and save that in a database (not defined yet).

## How project is organized?
Mainly, project is separated in api layer and web layer, but there's others layers:
- Api layer: Using [Nancy](http://nancyfx.org) I'm providing a Rest API with, for now, only one endpoint. It's only responsability is to call service to get trending repositories of today. Web will use that api, and I'm planning to later create a daemon to call that endpoint from time to time.
- Service layer: Service layer is referenced by API layer and is responsbile to take decisions about get trending topics from database or scrap that from Git trending page and Git API. Everytime this service get data from web, it also calls the repository to persist that data into database. Repository implementation is not part of that layer, it's part of infrastructure layer.
- Infrastrcuture layer: This has classes to persist data and read from database, also, all external access (like scrap and rest client) are implemented there. This is the only layer that should know [Html Agility Pack](http://html-agility-pack.net) and [RestSharp](http://restsharp.org). This layer is used by service layer.
- Domain layer: This layer has entities and repositories interfaces, all layers references that. As the domain of this application is really really small, this project isn't a good project to show all the power of Domain layer. But in every DDD project, this layer is the most decoupled layer and all other layers should be constructed around that. All business rules should stay there.

## SOLID principles
I'm trying to follow SOLID principles, API layer concentrates all settings. API layer, such as Web layer, are presentation layers, and these layers should contains dependency injection layers. By now, I'm using TinyIoc provided by Nancy, and I'm realling love work with [Nancy](http://nancyfx.org) due to it simplicity and ellegancy.
