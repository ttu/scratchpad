#https://www.digitalocean.com/community/tutorials/how-to-install-and-use-redis

sudo apt-get update 
#sudo apt-get -y install redis-server
sudo apt-get -y install build-essential
sudo apt-get install tcl8.5
 
wget http://download.redis.io/redis-stable.tar.gz
tar xvzf redis-stable.tar.gz
cd redis-stable
make

sudo make install

cd utils
sudo ./install_server.sh

sudo update-rc.d redis_6379 defaults

# If you want to copy own configuration files use these
#cp /vagrant/redis.init.d /etc/init.d/redis
#cp /vagrant/redis.conf /etc/redis.conf

redis-cli ping