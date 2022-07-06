token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjZWNpbGlhIiwianRpIjoiOGNiYWIwOTEtZTYwNC00MjhjLTk5MzEtYjJjZmI0MmQ3NDQ3IiwiaWF0IjoiMDUvMjcvMjAyMCAxMzo0ODo1MCIsIm5iZiI6MTU5MDU4NzMzMCwiZXhwIjoxNTkwNjMwNTMwfQ.mHEn2iIxo4gVv8mKu7cgzlcsmQEmqVUnLFel1RHcVXA



echo
echo "Adicionei o utilizador luis"

curl -v -X POST "http://localhost:8080/users" -H "accept: */*" -H "Content-Type: application/json" -d "{\"name\":\"luis\",\"username\":\"luis\",\"email\":\"luis@gmail.com\",\"nif\":\"123451\",\"password\":\"luis\",\"creditCards\":[{\"cardType\":\"master\",\"cardNumber\":\"123556\",\"expireDate\":\"2020-05-26T15:32:14.497Z\",\"cvv\":120}],\"favoriteChargingStations\":[\"string\"],\"trips\":[0],\"vehiclesIds\":[\"string\"]}"

echo
echo "Adicionei o utilizador joel"

curl -v -X POST "http://localhost:8080/users" -H "accept: */*" -H "Content-Type: application/json" -d "{\"name\":\"joel\",\"username\":\"joel\",\"email\":\"joel@gmail.com\",\"nif\":\"12351\",\"password\":\"joel\",\"creditCards\":[{\"cardType\":\"master\",\"cardNumber\":\"123556\",\"expireDate\":\"2020-05-26T15:32:14.497Z\",\"cvv\":120}],\"favoriteChargingStations\":[\"string\"],\"trips\":[0],\"vehiclesIds\":[\"string\"]}"

echo
echo "Adicionei o utilizador joao"

curl -X POST "http://localhost:8080/users" -H "accept: */*" -H "Content-Type: application/json" -d "{\"name\":\"joao\",\"username\":\"joao\",\"email\":\"joao@gmail.com\",\"nif\":\"12351\",\"password\":\"joao\",\"creditCards\":[{\"cardType\":\"master\",\"cardNumber\":\"123556\",\"expireDate\":\"2020-05-26T15:32:14.497Z\",\"cvv\":120}],\"favoriteChargingStations\":[\"string\"],\"trips\":[0],\"vehiclesIds\":[\"string\"]}"


echo
echo "Adicionei o utilizador goncalo"

curl -X POST "http://localhost:8080/users" -H "accept: */*" -H "Content-Type: application/json" -d "{\"name\":\"goncalo\",\"username\":\"goncalo\",\"email\":\"goncalo@gmail.com\",\"nif\":\"121\",\"password\":\"goncalo\",\"creditCards\":[{\"cardType\":\"master\",\"cardNumber\":\"123556\",\"expireDate\":\"2020-05-26T15:32:14.497Z\",\"cvv\":120}],\"favoriteChargingStations\":[\"string\"],\"trips\":[0],\"vehiclesIds\":[\"string\"]}"


echo
echo "Encontrar o utilizador cecilia"

curl -v -X GET "http://localhost:8080/users/cecilia" -H "accept: application/json" -H "Authorization: Bearer $token"


echo
echo "Encontrar todos os utilizadores"

curl -v -X GET "http://localhost:8080/users" -H "accept: application/json" -H "Authorization: Bearer $token"


echo
echo "Adicionar um cartao de credito ao utilizador joao"

curl -v -X POST "http://localhost:8080/users/joao/manage/creditCards" -H "accept: */*" -H "Content-Type: application/json" -d "{\"cardType\":\"visa\",\"cardNumber\":\"12345678\",\"expireDate\":\"2020-05-26T16:38:47.113Z\",\"cvv\":500}" -H "Authorization: Bearer $token"

echo
echo "Encontrar os cartoes de credito do utilizador joao"

curl -v -X GET "http://localhost:8080/users/joao/manage/creditCards" -H "accept: application/json" -H "Authorization: Bearer $token"


echo
echo "Remover o cartao de credito n. 12345678 do utilizador joao"

curl -X DELETE "http://localhost:8080/users/joao/manage/creditCards/12345678" -H "accept: */*" -H "Authorization: Bearer $token"



echo
echo "Adicionei uma favorite station ao perfil do utilizador joel"

curl -v -X POST "http://localhost:8080/users/joel/manage/favorites" -H "accept: */*" -H "Content-Type: application/json" -d "{\"chargingStationId\":\"BragaCity\"}" -H "Authorization: Bearer $token"


echo
echo "Adicionei uma favorite station ao perfil do utilizador joel"

curl -v -X POST "http://localhost:8080/users/joel/manage/favorites" -H "accept: */*" -H "Content-Type: application/json" -d "{\"chargingStationId\":\"BragaUniversity\"}" -H "Authorization: Bearer $token"


echo
echo "Remover  uma favorite station BragaUniversity  ao perfil do utilizador joel"

curl -v -X DELETE "http://localhost:8080/users/joel/manage/favorites/BragaUniversity" -H "accept: */*" -H "Authorization: Bearer $token"



echo
echo "Encontrar todas as favorites stations do utilizador joel"

curl -v -X GET "http://localhost:8080/users/joel/manage/favorites" -H "accept: application/json" -H "Authorization: Bearer $token"

echo
echo "Remover o utilizador luis"

curl -v -X DELETE "http://localhost:8080/users/luis" -H "accept: application/json" -H "Authorization: Bearer $token"


echo
echo "Encontrar  o utilizador goncalo"

curl -v -X GET "http://localhost:8080/users/goncalo" -H "accept: application/json" -H "Authorization: Bearer $token"

echo
echo "Actualizar o utilizador cecilia - mudei email de cecilia@gmail.com para ceciliaOliveira@gmail.com"

curl -v -X PUT "http://localhost:8080/users/cecilia" -H "accept: */*" -H "Content-Type: application/json" -d "{\"name\":\"cecilia\",\"username\":\"cecilia\",\"email\":\"ceciliaOliveira@gmail.com\",\"nif\":\"123456\",\"password\":\"cecilia\",\"creditCards\":[{\"cardType\":\"visa\",\"cardNumber\":\"string\",\"expireDate\":\"2020-05-26T16:27:09.264Z\",\"cvv\":125}],\"favoriteChargingStations\":[\"string\"],\"trips\":[0],\"vehiclesIds\":[\"string\"]}" -H "Authorization: Bearer $token"


echo
echo "Alterar password do utilizador cecilia - de cecilia passa a dennis"

curl -v -X PUT "http://localhost:8080/users/cecilia/manage/password" -H "accept: */*" -H "Content-Type: application/json" -d "{\"currentPassword\":\"cecilia\",\"newPassword\":\"dennis\"}" -H "Authorization: Bearer $token"


echo
echo "Encontrar o utilizador cecilia"

curl -v -X GET "http://localhost:8080/users/cecilia" -H "accept: application/json" -H "Authorization: Bearer $token"



echo
echo "Alterar password do utilizador cecilia - de dennis passa a cecilia"
curl -v -X PUT "http://localhost:8080/users/cecilia/manage/password" -H "accept: */*" -H "Content-Type: application/json" -d "{\"currentPassword\":\"dennis\",\"newPassword\":\"cecilia\"}" -H "Authorization: Bearer $token"


echo
echo "Encontrar o utilizador cecilia"

curl -v -X GET "http://localhost:8080/users/cecilia" -H "accept: application/json" -H "Authorization: Bearer $token"



echo
echo "Logout do user"
curl -v -X DELETE "http://localhost:8080/users/logout" -H "accept: */*" -H "Authorization: Bearer $token"

