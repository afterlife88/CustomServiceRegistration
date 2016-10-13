#Custom service registration
### DevChallenge 2016 fall, back-end qualification round

## Pull image of application from docker hub and run on 8080 port:
> - **docker run -p 8080:5000 devchallenge/custom-registration-service**
> - Run localhost:8080 in the browser, if the port is busy, change `8080` in `docker run` command to any available port and run again.

## Alternative way - build from sources and run:
> - `cd src\CustomServiceRegistration`
> - `bower install`
> - `cd ..\..\`
> - `docker build -t app .`
> - `docker run -p 8080:5000 -t app`

## Technologies used:
- **Backend** ASP.NET Core, Entity Framework Core, MS SQL, Swagger (Auto-generated documentation for API), XUnit, Moq.
- **Frontend:** AngularJS, Angular Material (for a beautiful view in Material design).

## Internal dependencies
- **CustomServiceRegistration** -> CustomServiceRegistration.Domain, CustomServiceRegistration.TokenProvider
- **CustomServiceRegistration.Tests** ->  CustomServiceRegistration
- **CustomServiceRegistration.TokenProvider** -> CustomServiceRegistration.Domain
