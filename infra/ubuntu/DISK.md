- S.M.A.R.T.(Self-Monitoring, Analysis, and Reporting Technology) 

```

smartctl --scan

# 상태
smartctl -H /dev/sda

# 디스크 정보
smartctl -i /dev/sda

# 디스크이 모든 정보
smartctl -a /dev/sda

# -d
# ccis
# 1: SCIS 번호: lsscis -g
smartctl -a -d ccis , 1 /dev/sda
smartctl -a -d ccis , 1/2/3 /dev/sda

# https://www.servermon.kr/board/board.html?code=servermon_board2&page=1&type=v&board_cate=&num1=999670&num2=00000&number=312&lock=N&srsltid=AfmBOooov4qPfTx0uY6O1rW9N2OJwjKak3CwkqvYEl5pgcLXG2zNeQJ1

# https://blog.naver.com/anysecure3/221636186842

df -h	  
sudo smartctl -H /dev/sdX
sudo smartctl -a /dev/sdX

sudo smartctl -t short /dev/sdX  # 짧은 테스트 (1~2분)
sudo smartctl -t long /dev/sdX   # 긴 테스트 (수십 분~몇 시간)

sudo smartctl -l selftest /dev/sdX
```

ID	| 속성 | 설명
--- | --- | ---
5	| Reallocated_Sector_Ct     | 배드 섹터로 재배치된 섹터 개수 (0이 정상, 1 이상이면 위험)
9	| Power_On_Hours            | 디스크 총 가동 시간 (시간 단위, 24×365 = 약 8760이 1년)
12	| Power_Cycle_Count         | 디스크 전원 ON/OFF 횟수
194	| Temperature_Celsius       | 디스크 온도 (일반적으로 30~50℃가 정상)
197	| Current_Pending_Sector    | 읽기/쓰기 오류가 발생한 섹터 수 (0이 정상, 1 이상이면 위험)
198	| Offline_Uncorrectable     | 복구할 수 없는 오류 발생 섹터 수 (0이 정상, 1 이상이면 위험)

- Reallocated_Sector_Ct > 0 → 배드 섹터 있음, 백업 필요
- Current_Pending_Sector > 0 → 디스크 고장 가능성 있음
- Offline_Uncorrectable > 0 → 복구 불가능한 오류 있음, 즉시 교체 고려


```
Raw_Read_Error_Rate
디스크 표면이로부터 데이터를 읽는 과정에서 문제가 있을때 발생
Reallocated_Sector_Ct
섹터에 문제가 생겨서 스페어영역의 섹터로 대체한 횟수

Seek_Error_Rate 
탐색 오류율

Spin_Retry_Count
최대rpm에 도달하기위해서 회전을 시도하는 횟수

Current_Pending_Sector
불안정적인 섹터로 스페어영역 섹터로 remapping을 준비중이거나 읽는 과정에 문제가 생긴 섹터

Offline_Uncorrectable
읽기/쓰기에 문제가 생긴 섹터, 즉 디스크 표면이 손상됨. 

UDMA_CRC_Error_Count
하드디스크 인터페이스를 통해 데이타 전송과정에 발생한 CRC 오류 횟수
```