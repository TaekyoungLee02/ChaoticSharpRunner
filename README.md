# ChaoticSharpRunner
## 목차
1. [프로젝트 소개](#프로젝트-소개)
2. [팀소개](#팀-소개)
3. [기능소개](#주요-기능)
4. [개발기간](#개발-기간)
5. [와이어프레임](#와이어-프레임)
6. [Trouble Shooting](#trouble-shooting)
7. [프로젝트를 마치며](#프로젝트를-마치며)

## 프로젝트 소개
ChaoticSharpRunner는 장애물을 피하며 생존하는 3D 런닝 게임입니다.

제한된 체력을 갖고 시작하며 장애물을 피해 코인을 먹고 점수를 올리는 것이 목표입니다.

게임 도중 아이템을 먹으면 플레이어에게 이로운 기능을 얻고 게임 플레이에 도움을 줍니다.

맵의 속도가 점점 올라가기 때문에 장애물을 피하는 난이도가 상승하며 플레이어의 반응속도가 중요한 요소로 작용합니다.

## 팀 소개
저희 21조는 무시무시한 개조는 적당히 하조라는 팀명처럼 마개조보단 3D 기능에 대한 이해와 구현에 초점을 맞춘 팀입니다.

## 주요 기능
- 맵
  
  시간이 지날수록 속도가 빨라집니다.
  
  랜덤으로 생성되는 무한맵

- 아이템
  
  게임 도중 생성되며 플레이어에게 이로운 효과를 줍니다.
  
  - 회복 : 플레이어 체력 회복
  
  - 코인 : 점수 추가
  
  - 자석 : 아이템 끌어당김
  
  - 장애물 제거 : 장애물을 제거

- 장애물
  
  플레이어의 이동을 막는 장애물
  
  플레이어의 체력을 감소 시킵니다
  
- 커스텀
  
  플레이어의 색을 커스텀 가능하며 날개 착용이 가능합니다.
  
- UI
  
  점수
  - 현재 체력과 최대 체력을 알려줍니다.
    
  - 최대 체력은 저장되어 게임을 다시 켜도 유지됩니다.
    
  생명력
  - 현재 체력을 알려줍니다.
    
  게임 소개
  - 게임의 필요한 기능을 시작전 알려줍니다.

- 플레이어
  
  좌우키(이동)와 스페이스바(점프)로 플레이어의 위치 변경합니다.
  
  장애물에 닿으면 체력이 깍이고 남은 체력이 없다면 게임 종료합니다.
  
## 개발 기간
- 2024년 10월 31일 ~ 2024년 11월 7일

## 와이어 프레임
![image](https://github.com/user-attachments/assets/6b728e6c-8111-467e-9ffe-2b5df865a61e)
![image](https://github.com/user-attachments/assets/5d871d79-7787-4b9a-8daf-442481a8b078)

## Trouble Shooting
맵 간격 벌어짐 현상 - 오브젝트의 생성 위치 계산을 줄이고 생성은 Update에서 이동은 FixedUpdate에서 실행

## 프로젝트를 마치며
팀장. 이태경 : 

팀원. 김지훈 : 

팀원. 이건호 : 

팀원. 김준식 : 좋은 팀원분들과 함께 프로젝트를 할 수 있어 즐거웠고, 개발하며 이해하지 못했던 부분을 알려주셔서 프로젝트 동안 정말 감사했습니다.
