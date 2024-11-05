# Basket Management ![.github/workflows/github.yml](https://github.com/AdemCatamak/BasketManagement/workflows/.github/workflows/github.yml/badge.svg?branch=master)

ReadMe dosyasının Türkçe haline [bu link](./ReadMe_tr.md) aracılığıyla ulaşabilirsiniz.

## __Running the Application__

You can run the application using the _docker-compose_ file located in the root directory, and it will be available at localhost:5000.

Note: If you wish to run and test the application with _docker-compose_, make sure that port 5000 is not being used by any other application.

If you want to debug via an IDE, you can create the SqlServer and RabbitMQ components that the application will need in a container with the command ```docker-compose -f docker-compose-test.yml up```. Also, to run the application in debug mode, [DotNet 5](https://dotnet.microsoft.com/download/dotnet/5.0) must be installed on your computer.

Note: In order to run the required dependencies with ```docker-compose-test.yml```, ports `31433, 45672, 55672` must not be used.

## __Techniques Used During Development__

This project was created with the [Event-Driven Programming](https://ademcatamak.medium.com/olaya-dayal%C4%B1-programlama-event-driven-programming-d6b7e2c0d948) technique. As I mentioned in my article, projects can be designed with as few dependencies as possible. In this way, the code is more flexible in terms of adding new features.

The layers in the project were created according to the DDD principles. You can access the  justifications for which classes with which responsibilities I put in which layers through the article [Layers in DDD Projects](https://ademcatamak.medium.com/layers-in-ddd-projects-bd492aa2b8aa).

I tried to obey to the REST architecture in the BasketManagement.WebApi section where users interact with our application. You can access the [5 Rules of REST Architecture](https://ademcatamak.medium.com/5-rules-of-rest-architecture-434abaf5db44) that I pay attention to when designing a web service according to the REST architecture via this link.

I used the specification design pattern when requesting data from the data storage medium. In this way, it is easier to reuse and combine criteria. You can access the advantages of [Specification Design Pattern](https://ademcatamak.medium.com/specification-design-pattern-c814649be0ef) and information about how I implemented it in C# via the link.

Since I needed different read and write models and did not want the lock operation on the write to affect the read operation, I used the [CQRS Pattern](https://ademcatamak.medium.com/cqrs-command-query-responsibility-segregation-476d2d81225a). For sample implementation and explanations of this pattern, you can check out my article [Stock Management with CQRS](https://ademcatamak.medium.com/stok-y%C3%B6netimi-cqrs-%C3%B6rne%C4%9Fi-c8243b82c7b2).

When sending messages to the RabbitMQ message broker, the [Outbox Design Pattern](https://ademcatamak.medium.com/outbox-design-pattern-57e1519ed5e4) was used. Thus, the action of sending messages regarding changes in the system to RabbitMQ was included in the same Transaction Scope. In other words, changes in the system were recorded to be transmitted to the RabbitMQ system later, and it was guaranteed that no message loss would occur.

### __Stock__

This module manages the stock data of the products. All changes made to the product stock are stored in the database table called StockAction. These actions are reflected in the table called StockSnapshot within the same Transaction Scope. StockSnapshot stores the usable value of a product. If this value becomes negative, an error will be generated indicating that there is not enough stock. In this case, the database transactions will be Rollbacked. Thanks to Rollback, the data written to the StockSnapshot table will not be recorded.

The changes made on StockSnapshot are reflected asynchronously to the table named Stock. In this way, reading and writing models are separated. While the orders for the changes to be made on the stock are transferred to the system with the StockAction model, reading needs are met with the Stock model.

In order not to stray too far from the topic in the BasketManagement project, I did not include endpoints that would allow actions to be taken on the stock via WebApi. In case you need these endpoints, you can access the article I previously prepared as a PoC [Stock Management with CQRS](https://ademcatamak.medium.com/stok-y%C3%B6netimi-cqrs-%C3%B6rne%C4%9Fi-c8243b82c7b2) and the sample project.

### __Basket__

Authentication operations are not implemented to ensure that the application can be easily used and tested. For this reason, the tester can use any `AccountId` value he/she wants. Under normal conditions, this value should be checked and it should be checked whether the person sending the request can execute operations for the `AccountId` mentioned in the request.

#### __POST accounts/{accountId/}baskets__

It requests a value that will indicate the user. A new basket is created for the user with each request.

#### __GET accounts/{accountId/}baskets__

It requests a value that will indicate the user. It returns the baskets and the contents of the basket belonging to this user as a response. Optionally, a special basket information belonging to the user can be obtained by giving the `BasketId` value. If a basket that meets the criteria is not found, a response will be returned to the user request at 4XX level.

#### __PUT accounts/{accountId/}baskets/{basketId}__

The product and quantity value information to be included in the basket are entered in the request body. If the product is not in the basket, it will be added. If the product is in the basket, the quantity value will be updated. If the basket cannot be found, the user will be informed that the request is incorrect.

Note: Entering 0 for the quantity value is equivalent to removing the product from the cart.

#### __DELETE accounts/{accountId/}baskets/{basketId}/products/{productId}__

The user's cart information will be searched. If the cart information is found, it will be checked whether the mentioned product is in the cart. If the product is in the cart, the product will be removed from the cart. If the cart or product cannot be found, the error will be reflected to the user.

#### __DELETE accounts/{accountId/}baskets/{basketId}__

The user's cart information will be searched. If the cart information is found, all products in the mentioned cart will be removed from the cart and then the cart will be deleted. If the cart cannot be found, the error will be reflected to the user.

## Test

The entire project was not tested with unit or integration testing. Instead, tests were written to cover part of the domain and application layers to provide an example of how the need for unit and integration testing could be met.

The application layer is where decisions are made about communicating with external systems such as data storage. Unit tests should not communicate with components such as the network and file system. For this reason, I used the `Moq` library when preparing my tests for this layer. Thanks to the `Moq` library, I was able to create interfaces that communicate with external systems from fake objects that would behave as I wanted.

Integration tests were also written by creating external dependencies via docker. Thanks to the management of external dependencies via docker, the creation and destruction of environments for integration tests can be easily done in the CI pipeline. Test containers were used for this. For the implementation of this development, you can take a look at the article [Integration Test with Docker](https://ademcatamak.medium.com/integration-test-with-net-core-and-docker-21b241f7372). You can also find the creation of the CI pipeline at the code level. For this, the [Cake Build](https://ademcatamak.medium.com/cake-build-nedir-684eb1885b06) tool was used.
