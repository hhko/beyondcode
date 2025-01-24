---
outline: deep
---

# 도커

## 폴더 구성
```shell
# 폴더 구성
/home/{조직}
    /{프로젝트}
        /instll
            /docker-27.3.1              # 도커 설치 파일
            /docker-27.3.1-plugin       # 도커 Plugin 설치 파일
            /docker-compose-2.29.7      # 도커 컴포즈 설치 파일
        /data
            /docker                     # 도커 볼륨 경로: /var/lib/docker 이전 경로
        /images
            /1.0.1-poc.3.2024.11.12
            /1.0.1-poc.3.2024.11.12-infra

        .env
        docker-compose.yml
        docker-compose.infra.yml
        docker-compose.install.sh
```

## 오프라인 도커 설치 준비
### 도커 저장소 추가
```shell
#
# 1. 도커 저장소 추가
#

# 패키지 업데이트
sudo apt update

# 패키지 저장소 추가
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg

echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

# 패키지 업데이트: Docker 저장소 추가 후
sudo apt update

# docker 버전 이력
sudo apt list --all-versions docker-ce
```

### 도커 파일 다운로드
```shell
#
# 2. 도커 파일 다운로드
#

# docker deb 파일 다운로드: docker-ce=5:27.3.1-1~ubuntu.22.04~jammy일 때
apt download \
   $(apt depends \
         --recurse \
         --no-recommends \
         --no-suggests \
         --no-conflicts \
         --no-breaks \
         --no-replaces \
         --no-enhances \
         docker-ce=5:27.3.1-1~ubuntu.22.04~jammy \
      | grep "^\w" \
      | sort -u)
```
1. containerd.io_1.7.22-1_amd64.deb
1. docker-ce-cli_5%3a27.3.1-1~ubuntu.22.04~jammy_amd64.deb
1. docker-ce_5%3a27.3.1-1~ubuntu.22.04~jammy_amd64.deb
1. gcc-12-base_12.3.0-1ubuntu1~22.04_amd64.deb
1. iptables_1.8.7-1ubuntu5.2_amd64.deb
1. libc6_2.35-0ubuntu3.8_amd64.deb
1. libcap2_1%3a2.44-1ubuntu0.22.04.1_amd64.deb
1. libcrypt1_1%3a4.4.27-1_amd64.deb
1. libgcc-s1_12.3.0-1ubuntu1~22.04_amd64.deb
1. libgcrypt20_1.9.4-3ubuntu3_amd64.deb
1. libgpg-error0_1.43-3_amd64.deb
1. libip4tc2_1.8.7-1ubuntu5.2_amd64.deb
1. libip6tc2_1.8.7-1ubuntu5.2_amd64.deb
1. liblz4-1_1.9.3-2build2_amd64.deb
1. liblzma5_5.2.5-2ubuntu1_amd64.deb
1. libmnl0_1.0.4-3build2_amd64.deb
1. libnetfilter-conntrack3_1.0.9-1_amd64.deb
1. libnfnetlink0_1.0.1-3build3_amd64.deb
1. libnftnl11_1.2.1-1build1_amd64.deb
1. libseccomp2_2.5.3-2ubuntu2_amd64.deb
1. libsystemd0_249.11-0ubuntu3.12_amd64.deb
1. libxtables12_1.8.7-1ubuntu5.2_amd64.deb
1. libzstd1_1.4.8+dfsg-3build1_amd64.deb
1. netbase_6.3_all.deb

### 도커 Plugin 파일 다운로드
```shell
#
# 3. 도커 Plugin 파일 다운로드: https://download.docker.com/linux/ubuntu/dists/jammy/pool/stable/amd64/
#

sudo curl -L "https://download.docker.com/linux/ubuntu/dists/jammy/pool/stable/amd64/docker-buildx-plugin_0.17.1-1~ubuntu.22.04~jammy_amd64.deb" -o ./docker-buildx-plugin_0.17.1-1~ubuntu.22.04~jammy_amd64.deb
sudo curl -L "https://download.docker.com/linux/ubuntu/dists/jammy/pool/stable/amd64/docker-compose-plugin_2.29.7-1~ubuntu.22.04~jammy_amd64.deb" -o ./docker-compose-plugin_2.29.7-1~ubuntu.22.04~jammy_amd64.deb
sudo curl -L "https://download.docker.com/linux/ubuntu/dists/jammy/pool/stable/amd64/docker-ce-rootless-extras_27.3.1-1~ubuntu.22.04~jammy_amd64.deb" -o ./docker-ce-rootless-extras_27.3.1-1~ubuntu.22.04~jammy_amd64.deb
sudo curl -L "https://download.docker.com/linux/ubuntu/dists/jammy/pool/stable/amd64/docker-scan-plugin_0.23.0~ubuntu-jammy_amd64.deb" -o ./docker-scan-plugin_0.23.0~ubuntu-jammy_amd64.deb
```

1. docker-buildx-plugin_0.17.1-1~ubuntu.22.04~jammy_amd64.deb
1. docker-ce-rootless-extras_27.3.1-1~ubuntu.22.04~jammy_amd64.deb
1. docker-compose-plugin_2.29.7-1~ubuntu.22.04~jammy_amd64.deb
1. docker-scan-plugin_0.23.0~ubuntu-jammy_amd64.deb

### 도커 컴포즈 파일 다운로드
```shell
#
# 4. 도커 컴포즈 파일 다운로드
#
# https://github.com/docker/compose/releases
https://github.com/docker/compose/releases/download/v2.29.7/docker-compose-linux-x86_64
sudo curl -L "https://github.com/docker/compose/releases/download/v2.29.7/docker-compose-$(uname -s)-$(uname -m)" -o ./docker-compose
```
- docker-compose


## 오프라인 도커 설치
### 도커 설치
```shell
#
# 1. 도커 설치
#
mkdir -p ~/{프로젝트}/install/docker-27.3.1/
# 파일 복사

cd ~/{프로젝트}/install/docker-27.3.1/
sudo dpkg -i ./*.deb
docker -v
```

### 도커 Plugin 설치
```shell
#
# 2. 도커 Plugin 설치
#
mkdir -p ~/{프로젝트}/install/docker-27.3.1-plugin/
# 파일 복사

cd ~/{프로젝트}/install/docker-27.3.1-plugin/
sudo dpkg -i ./*.deb
```

### 도커 그룹 추가
```shell
#
# 3. 도커 그룹 추가
#
id
sudo usermod -aG docker $USER

sudo systemctl reboot					# 또는 `sudo -su $USER`
id
```

### 도커 볼륨 변경
```shell
#
# 4. 도커 볼륨 변경
#
docker info | grep Root
sudo systemctl status docker

df -h
findmnt -T /lib/systemd/system

mkdir -p ~/{프로젝트}/data/docker
findmnt -T ~/{프로젝트}/data/docker

# /home/mirero/{프로젝트}/data/docker 경로 확인
sudo vi /lib/systemd/system/docker.service
ExecStart=/usr/bin/dockerd -H fd:// --containerd=/run/containerd/containerd.sock --data-root=/home/mirero/{프로젝트}/data/docker

sudo systemctl stop docker
sudo systemctl daemon-reload
sudo systemctl start docker

docker info | grep Root
```

### 도커 컴포즈 설치
```shell
#
# 5. 도커 컴포즈 설치
#
mkdir -p ~/{프로젝트}/install/docker-compose-2.29.7/
# 파일 복사

cd ~/{프로젝트}/install/docker-compose-2.29.7/
sudo cp ./docker-compose /usr/local/bin/docker-compose

sudo chmod +x /usr/local/bin/docker-compose
ls -al /usr/local/bin/docker-compose

docker compose -v
```

## 로그 삭제
- TODO

## 타임 서버
- TODO