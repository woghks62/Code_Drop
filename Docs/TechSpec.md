# Tech Specification

> 이 프로젝트의 기술 스택 관련 결정 사항이다.
> Claude가 시스템 기술 문서를 작성·검수할 때 이 문서를 기준으로 한다.

---

## 프로젝트 제원

- 프로젝트명 : Code: Drop
- 장르 : 탑다운 액션 슈팅
- 팀 구성 : 1인

## 유니티 환경

- 버전 : 6000.3.9f1 LTS
- 렌더 파이프라인 : URP
- 빌드 플랫폼 : Windows

## 유저 입력 시스템

- New Input System을 사용한다.
  - 구 `InputManager`는 사용하지 않는다.
- 특정 입력에 대한 기능 구현 시 `InputActionReference`를 사용해 `InputAction`을 참조하는 형태로 구현한다.
- 일반 조작과 스트라타잼 코드 입력은 `Input Action Map`을 활용해 구분한다.