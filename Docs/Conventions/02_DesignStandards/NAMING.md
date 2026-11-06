# 네이밍 규칙

> 개발자(사용자)가 코드를 작성할 때 따르는 네이밍 컨벤션.
>
> Claude는 이 문서로 코드를 작성하지 않으며, 기술 문서 작성·검수와 힌트 제시의 근거로만 참조한다.

## 02. 네이밍 규칙

### 02-1. C# 이름
| 대상 | 규칙 | 예시 |
|---|---|---|
| 클래스·구조체·열거형 | PascalCase | `RockController`, `GameState` |
| 열거형 멤버 | PascalCase | `Ready`, `GameOver` |
| 인터페이스 | `I` + PascalCase | `IDamageable` |
| 메서드·프로퍼티 | PascalCase | `MoveToTarget()`, `CurrentHealth` |
| private / protected 필드 | `_camelCase` | `_moveSpeed` |
| public 필드 | 사용하지 않음 (예외: 싱글톤 `public static` 필드는 PascalCase) | `Instance` |
| 지역 변수·매개변수 | camelCase | `targetPosition` |
| 상수 | UPPER_SNAKE_CASE | `MAX_HEALTH` |
| 이벤트 | `On` + 과거형 | `OnDied`, `OnScoreChanged` |
| 이벤트 핸들러 | 구독하는 이벤트와 같은 이름 | `OnScoreChanged` |
| 입력 액션 핸들러 (예외) | `On` + 액션 이름 | `OnMove`, `OnAttack` |
| bool 상태·조건 | `Is`, `Has`, `Can`, `Should` 등 | `_isMoving`, `IsMoving`, `CanAttack` |

- 접근 제한자(`private`, `protected`, `public` 등)는 생략하지 않고 명시한다.
- 필드의 외부 공개 여부는 `CODE_STYLE.md` 01-3을 따른다.

### 02-2. 클래스 역할 접미사
- `Controller`, `Manager`, `Data`, `Finder`, `Calculator` 등은 실제 역할에 맞을 때만 사용한다.
- 모든 클래스에 습관적으로 `Manager`를 붙이지 않는다.

### 02-3. 에셋 접두사
| 에셋 종류 | 접두사 | 예시 |
|---|---|---|
| Script / Scene | 없음 | `RockController.cs`, `GameScene.unity` |
| Prefab | `PF_` | `PF_Enemy` |
| Material | `MAT_` | `MAT_Rock` |
| Texture | `TEX_` | `TEX_Ground` |
| Sprite | `SPR_` | `SPR_Icon` |
| Animation Clip | `ANIM_` | `ANIM_Run` |
| Animator Controller | `AC_` | `AC_Player` |
| 효과음 | `SFX_` | `SFX_Hit` |
| 배경음악 | `BGM_` | `BGM_Battle` |
| ScriptableObject 에셋 | `SO_` | `SO_EnemyData` |
| Physics Material | `PM_` | `PM_Ice` |
| Input Actions | `IA_` | `IA_Player` |

- 에셋 이름은 영어 PascalCase를 사용하고 공백을 넣지 않는다.

### 02-4. 폴더 및 namespace
- 폴더 이름은 PascalCase를 사용한다.
- namespace는 사용하지 않는다. 클래스는 파일 최상단에서 바로 선언한다.
- 외부 에셋과 클래스 이름이 충돌하면, 그때 해당 스크립트만 수정해 해결한다.
