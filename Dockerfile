FROM microsoft/dotnet:latest

# Set env variables
ENV ASPNETCORE_URLS http://*:5000

COPY /src/CustomServiceRegistration /app/src/CustomServiceRegistration
COPY /src/CustomServiceRegistration.Domain /app/src/CustomServiceRegistration.Domain
COPY /src/CustomServiceRegistration.TokenProvider /app/src/CustomServiceRegistration.TokenProvider

# Restore domain
WORKDIR /app/src/CustomServiceRegistration.Domain
RUN ["dotnet", "restore"]

# Restore token provider 
WORKDIR /app/src/CustomServiceRegistration.TokenProvider
RUN ["dotnet", "restore"]

WORKDIR /app/src/CustomServiceRegistration
RUN ["dotnet", "restore"]
RUN ["dotnet", "build"]
 
# Open port
EXPOSE 5000/tcp
 
ENTRYPOINT ["dotnet", "run"]