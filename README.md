# MyAppKafka
MyApp is an order creating app for real time communication using Kafka

Here is provided YAML file 

Need to run docker image for Confluent/kafka with ui akhq-ui

Run in terminal -->
- docker compose up -d
  To download image and run containers

- docker compose down -v
  To stop/delete running container

- docker-compose logs -f kafka
  To check if kafka is running with its necessary containers

- docker-compose logs -f akhq
  To check if kafka ui akhq is running smoothly
  
- docker system prune -f
  To delete cached remaining on it
