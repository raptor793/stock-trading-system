## How to build:

1. Clone the repository
2. Run docker-compose up --build from the directory of the project, where there is a docker-compose file

3. After a successful build se these links to access the gateway:
Order Service - http://localhost:5000/orderservice/swagger/index.html
Portfolio Service - http://localhost:5000/portfolioservice/swagger/index.html

4. For viewing database tables and data you can use for example: DBeaver
Order database connection string - "Host=order-db;Port=5432;Database=OrderDb;Username=postgres;Password=postgres;Include Error Detail=true;"
Portfolio database connection string - "Host=portfolio-db;Port=5432;Database=PortfolioDb;Username=postgres;Password=postgres;Include Error Detail=true;"

5. RabitMQ UI - http://localhost:15672/ Username and Password "guest"

6. When adding new stocks pass for side parameter buy or sell exactly in order for it to work
