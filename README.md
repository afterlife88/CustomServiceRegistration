#Custom service registration
### DevChallenge 2016 fall, back-end qualification round

## Pull image of application from docker hub and run on 8080 port:
> - **docker run -p 8080:5000 devchallenge/custom-registration-service**
> - Запустите localhost:8080 в браузере, если порт занят, замените в docker run 8080 на любой другой порт и запустите команду еще раз.

## Alternative way - build from sources and run:
> - `cd src\CustomServiceRegistration`
> - `bower install`
> - `cd ..\..\`
> - `docker build -t app .`
> - `docker run -p 8080:5000 -t app`

## Technologies used:
- **Backend** ASP.NET Core, Entity Framework Core, MS SQL, Swagger (для авто документации API), XUnit, Moq.
- **Frontend:** AngularJS, Angular Material (для красивого отображения в стиле Material design).

## Internal dependencies
- **CustomServiceRegistration** -> CustomServiceRegistration.Domain, CustomServiceRegistration.TokenProvider
- **CustomServiceRegistration.Tests** ->  CustomServiceRegistration
- **CustomServiceRegistration.TokenProvider** -> CustomServiceRegistration.Domain
