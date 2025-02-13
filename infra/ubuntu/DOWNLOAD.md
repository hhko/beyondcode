```
sudo sed -i 's/archive.ubuntu.com/old-releases.ubuntu.com/g' /etc/apt/sources.list
sudo sed -i 's/security.ubuntu.com/old-releases.ubuntu.com/g' /etc/apt/sources.list

sudo apt clean
sudo apt update
sudo apt upgrade -y


https://old-releases.ubuntu.com/ubuntu/pool/main/g/glibc/


wget http://old-releases.ubuntu.com/ubuntu/pool/main/g/glibc/libc6_2.27-3ubuntu1.3_amd64.deb
sudo dpkg -i libc6_2.27-3ubuntu1.3_amd64.deb
sudo apt -f install
```
