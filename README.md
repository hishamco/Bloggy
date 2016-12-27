# Bloggy

## Why another blog engine?

> It's always time to start a new blog engine

That's the retweet from Mads Kristensen @madskristensen after I tweeted him on 7 May 2016 to start a new blog engine using the latest and greates technologies. Unfortunately he don't have a time!! I'm sure that he is busy with building a new cool extensions for Visual Studio as usual :)

I take the initiative to continue with what he built last few years in both BlogEngine & MiniBlog, so I decide to build a new blog engine called **bloggy** using the new ASP.NET Core.

I'm sure this is a great lesson for me to use almost the things that I know and love in ASP.NET Core to build such application.

## Features

- Performance
  - Response Compression
  - Response Caching
  - Support CDN for scripts & styles in Production Environment
- Authentication
  - Cookie-based authentication
  - Social Media authentication
- Configuration
  - Almost the blog settings are configurable in appsettings.json
- Themable
  - Easy to create and customize the themes
  - The conjunction with localization make it easy to support different layouts LTR & RTL
  - Comes with light & black theme
- Localization
  - Support both English & Arabic out of the box
  - Url Culture Provider
- Schedule posts to be published on a future date
- Comments
  - Gravatar support
  - reCaptcha support
  - Comment moderation
  - Live preview on commenting
- Data Store
  - Shipped with InMemory (for Testing) & SQLite, but you can easily switch to SQL Server or any EntityFramework Database Provider
- Web Feeds
  - RSS
  - ATOM
- Markdown support as first class citizen
- Mobile friendly
- Works on Azure