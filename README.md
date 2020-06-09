# Pilllar System

Docker <br>
docker build -t pilllar .<br>
docker run -it --rm --name pilllar -p 8000:80 pilllar:latest<br>

Open browser -> localhost:8000

Accessing on Windows through cmd<br>
docker exec -t -i Pilllar.Admin.WebApi cmd

or 

docker-compose up -d

<p>
getting the ip´s
<p>
<b>docker inspect -f '{{.Name}} - {{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' $(docker ps -aq)</b>
