- S.M.A.R.T.(Self-Monitoring, Analysis, and Reporting Technology) 

```
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