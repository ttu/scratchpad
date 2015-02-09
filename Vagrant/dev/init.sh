#!/bin/bash
# Using Precise32 Ubuntu

sudo apt-get update
#
# For PHP 5.5
#
sudo apt-get install -y python-software-properties
sudo add-apt-repository ppa:ondrej/php5
sudo apt-get update

#
# MySQL with root:<no password>
#
export DEBIAN_FRONTEND=noninteractive
apt-get -q -y install mysql-server

#
# PHP
#
sudo apt-get install -y php5 php5-dev apache2 libapache2-mod-php5 libpcre3-dev
sudo apt-get install -y php5-mcrypt php5-curl php5-intl php5-mysql

#
# Redis
#
sudo apt-get install -y redis-server

#
# MongoDB
#
sudo apt-get install -y mongodb-clients mongodb-server

#
# Utilities
#
sudo apt-get install -y make curl htop git-core vim

#
# Redis Configuration
# Allow us to Remote from Vagrant with Port
#
sudo cp /etc/redis/redis.conf /etc/redis/redis.bkup.conf
sudo sed -i 's/bind 127.0.0.1/bind 0.0.0.0/' /etc/redis/redis.conf
sudo /etc/init.d/redis-server restart

#
# MySQL Configuration
# Allow us to Remote from Vagrant with Port
#
sudo cp /etc/mysql/my.cnf /etc/mysql/my.bkup.cnf
# Note: Since the MySQL bind-address has a tab cahracter I comment out the end line
sudo sed -i 's/bind-address/bind-address = 0.0.0.0#/' /etc/mysql/my.cnf

#
# Grant All Priveleges to ROOT for remote access
#
mysql -u root -Bse "GRANT ALL PRIVILEGES ON *.* TO 'root'@'%' IDENTIFIED BY '' WITH GRANT OPTION;"
sudo service mysql restart

#
# Composer for PHP
#
#sudo curl -sS https://getcomposer.org/installer | php
#sudo mv composer.phar /usr/local/bin/composer

#
# Apache VHost
#
cd ~
echo '<VirtualHost *:80>
        DocumentRoot /vagrant/www
</VirtualHost>

<Directory "/vagrant/www">
        Options Indexes Followsymlinks
        AllowOverride All
        Require all granted
</Directory>' > vagrant.conf

sudo mv vagrant.conf /etc/apache2/sites-available
sudo a2enmod rewrite

#
# Reload apache
#
sudo a2ensite vagrant
sudo a2dissite 000-default
sudo service apache2 reload
sudo service apache2 restart
sudo service mongodb restart
