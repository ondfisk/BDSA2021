# Docker

## Run container

```bash
$password = New-Guid
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=$password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest
```

## List running containers

```bash
docker ps
```

## Enter Container

```bash
docker exec -it container_id_or_name /bin/bash
```

## Kill all running containers

```powershell
docker ps | Select-String -Pattern "^[0-9a-f]+" -CaseSensitive | ForEach-Object { docker kill $PSItem.Matches.Value }
```

## Remove all exited containers

```powershell
docker container list --all --filter STATUS=exited | Select-String -Pattern "^[0-9a-f]+" -CaseSensitive | ForEach-Object { docker container rm $PSItem.Matches.Value }
```

## Remove all unused local volumes

```bash
docker volume prune --force
```

## Remove unused images

```bash
docker image prune --force
```
