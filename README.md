# lvq-aspnet-microservices

This repo was inspired by Les Jackson. Building basic about .NET Microservices system.

## What are building
- Building two .NET Microservices using the REST API pattern
- Working with dedicated persistence layers for both services
- Deploying our services to K8s
- Employing the API gateway pattern to route our services
- Building Synchronous messaging between services (HTTP and gRPC)
- Building Asynchronous messaging between services using an Even Bus (RabbitMQ)

## Solution Architecture
![image](https://user-images.githubusercontent.com/26388126/231938119-2e9d7d46-e339-47cd-a70b-b677c6079e9f.png)

## The progress building
### Platforms Service
- [x]  Start services
- [x] Containerize service
- [x] Push service to Docker hub
- [x] Deploy Platform service (Kubernetes)

### Commands Service
- [x] Start services
- [x] Overview of synchronous and asynchronous messaging
- [x] Adding HTTP client
- [x] Containerize CommandsService (2nd service)

### Deploying
- [x] Deploying services to K8s
- [x] Adding an API gateway (Ingress controller) 

References:
1. [What is an Ingress controller](https://www.nginx.com/resources/glossary/kubernetes-ingress-controller/)
2. Look at **Get started** to config Ingress controller: https://github.com/kubernetes/ingress-nginx
3. Error about access Nginx ingress controller on Local: https://stackoverflow.com/questions/59255445/how-can-i-access-nginx-ingress-on-my-local

- [x] Starting with SQL server
  - [x] Adding a Kubernetes secret
  - [x] Deploying SQL server to K8s
  - [x] Accessing SQL server via a management tool

References:
1. K8s persistent Volumes: https://kubernetes.io/docs/concepts/storage/persistent-volumes/
2. K8s secrets: https://kubernetes.io/docs/concepts/configuration/secret/

- [x] update our Platform Service using SQL server
  - Update and deploy the Dockerfile Platform service
  - Restart the deployment Platform service

References:
1. Install dotnet-ef global: https://learn.microsoft.com/en-us/ef/core/cli/dotnet 
2. If you get an error pre-login handshake, look at: https://github.com/dotnet/SqlClient/issues/1479

- [x] Multi-Resources APIs (Do Dbcontext, models, dto, repo, action & controller, etc on Command service)
- [x] Message Bus, RabbitMQ
  - [x] Add a Message Bus to **Platform Service**
    - [x] Testing our Publisher  
  - [x] Event processing on **Command Service**
  - [x] Adding an Event listener on **Command Service**
  - [x] Deploying to K8s
  
- [x] Sync communication gRPC
  - [x] Add config k8s ClusterIP for platformgrpc on **Platform Service**  
  - [x] Add gRPC packages
  - [x] Working with Protocol Buffers
  - [x] Adding a gRPC server to Platforms Service
  - [x] Adding a  gRPC client to Commands Service

- [ ] Write Docker compose
- [ ] Implement Identiy Service
- [ ] Implement Log service (use Elastic search)
- [ ] Implement [Cache] service (use Redis cache)
