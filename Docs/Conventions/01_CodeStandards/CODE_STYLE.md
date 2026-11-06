# C# 코딩 스타일

> 개발자(사용자)가 코드를 작성할 때 따르는 C# 코딩 스타일 컨벤션.
>
> Claude는 이 문서로 코드를 작성하지 않으며, 기술 문서 작성·검수와 힌트 제시의 근거로만 참조한다.

## 01. C# 코딩 스타일

### 01-1. 중괄호 및 간결한 표현
- `if`, `else`, `for`, `foreach`, `while`에는 한 줄이어도 중괄호를 사용한다.
- 읽기 전용 프로퍼티는 식 본문(`=>`)으로 작성한다. 예: `public int Health => _health;`
- 메서드는 처리가 한 줄이어도 블록 본문(`{ }`)으로 작성한다. 단, 아래 Unity 생명주기 메서드 규칙은 예외로 한다.
- Unity 생명주기 메서드의 본문 형식은 단계에 따라 다르게 작성한다.
  - 시작·종료 단계(`Awake`, `OnEnable`, `Start`, `OnDisable`, `OnDestroy`): 메서드 하나만 호출하면 식 본문으로 작성한다. 예: `private void OnEnable() => BindInputEvents();`
    - 다른 처리가 추가되면 블록 본문(`{ }`)으로 바꾼다.
  - 반복 단계(`Update`, `FixedUpdate`, `LateUpdate`): 처리가 늘어나는 경우가 많으므로 한 줄이어도 블록 본문으로 작성한다.

### 01-2. 타입 선언
- 기본적으로 명시적인 타입을 사용한다.
- 우변에서 타입을 확인할 수 있어도 `var`를 사용하지 않는다. 예: `List<Product> products = new List<Product>();`
- 익명 타입처럼 명시적 타입 선언이 불가능한 경우 `var`를 사용한다.

### 01-3. 외부 공개 상태
- 기본적으로 필드는 `private`으로 선언한다.
- Inspector에서 설정할 값은 `[SerializeField] private`을 사용하고, `[SerializeField]`는 필드와 같은 줄에 붙여 작성한다.
  - 예: `[SerializeField] private float _moveSpeed = 5f;`
- 외부에 읽기만 허용할 값은 읽기 전용 프로퍼티로 제공한다.
  - 예: `public int Health => _health;`
- 게임 상태는 외부에서 직접 수정하지 못하도록 한다.
- 값 변경 시 검증, 이벤트 발생, 다른 상태 변경 등의 처리가 필요하면 의미 있는 메서드를 통해 변경한다.
- 저장 데이터(DTO) 등 **값을 보관하거나 전달하는 역할만 하는 클래스**에서는 필요에 따라 `public setter`를 허용한다.
- 단순 데이터 클래스라도 값 변경에 검증이나 추가 로직이 필요하다면 변경 메서드를 사용한다.
- 역할이 불분명하거나 판단하기 어려운 경우에는 외부 수정을 제한하는 방식을 우선한다.
- 기존 클래스의 접근 수준을 변경할 때는 해당 멤버를 사용하는 곳을 먼저 확인한다.

### 01-4. 이벤트
- 이벤트는 `public event Action OnDied;`, `public event Action<int> OnHealthChanged;`처럼 선언한다.
- 이벤트와 이벤트 처리 메서드(핸들러)의 이름은 `NAMING.md` 02-1을 따른다.
- 발생 시 `?.Invoke(...)`를 사용한다.
- 일반적으로 `OnEnable`에서 구독하고 `OnDisable`에서 해제한다. 비활성 중에도 이벤트를 받아야 한다면 수명에 맞게 조정한다.
- Input System 입력은 `ReadValue` 폴링 대신 액션 콜백(`performed`, `canceled` 등)을 구독해 받는다. 이동처럼 계속 이어지는 입력은 콜백에서 값을 필드에 저장하고 `Update`에서 사용한다.
- 입력 이벤트 구독은 `private void BindInputEvents()`, 해제는 `private void UnBindInputEvents()` 메서드로 분리한다.
  - `OnEnable`/`OnDisable`의 본문 형식은 01-1의 생명주기 메서드 규칙을 따른다.

### 01-5. 클래스 멤버 순서
1. 상수 및 static 멤버
2. `[SerializeField]` 필드
3. 일반 private 필드
4. 프로퍼티
5. 이벤트
6. Unity 생명주기 메서드 (`OnDrawGizmos` 계열 제외)
7. public 메서드
8. private 메서드 (10번 초기화 메서드 제외)
9. 이벤트 핸들러
10. 초기화 메서드 (`CacheComponents` → `BindInputEvents` → `UnBindInputEvents` 순)
    - 없는 메서드는 건너뛰고 나머지 순서를 유지한다.
11. 에디터 디버그용 메서드 (`OnDrawGizmos`, `OnDrawGizmosSelected`)
    - Scene 뷰 확인용으로 게임 동작과 무관하므로 클래스 맨 아래에 둔다.

### 01-6. 주석
- 주석 작성 규칙은 `COMMENT_GUIDELINES.md`를 따른다.

### 01-7. null 및 오류 처리
- 필수 참조가 누락되면 `Debug.LogError(..., this)`로 알리고 해당 기능의 진행을 중단한다.
- 선택 참조는 null일 경우 안전하게 건너뛸 수 있다.
- 파괴된 Unity 오브젝트의 null 판정에는 `== null`을 사용한다. `UnityEngine.Object`에 `?.`, `??`를 무조건 적용하지 않는다.
- `try/catch`는 예외 발생 가능성이 있는 외부 작업(파일 입출력 등)에만 사용한다.
  - 예외를 숨기는 빈 `catch`는 사용하지 않는다.

### 01-8. Try 패턴
- 호출자가 성공 여부를 알아야 할 때 `bool TryX(...)`를 사용한다.
- 결과 데이터가 필요하면 `out` 매개변수를 사용한다.

### 01-9. 조건 분기
- 중첩을 줄이기 위해 가드 절(early return)을 사용한다.
  - 가드 조건 사이에 다른 처리가 없으면 `||`로 묶어 하나의 가드 절로 작성한다.
  - 가드 조건 사이에 다른 처리가 있으면 각각 따로 작성한다.
- 단순한 분기에서는 `if/else`를 허용한다.
- 조건에 따라 값 하나를 고르는 경우에는 삼항 연산자를 사용한다. 예: `_currentSpeed = _isRunning ? _runSpeed : _walkSpeed;`
  - 분기마다 메서드 호출 등 처리가 있으면 `if/else`를 사용한다.

### 01-10. 컬렉션 및 반복문
- 용도에 맞게 배열, `List`, `Dictionary`, `HashSet`, `Queue`, `Stack`을 선택한다.
  - `Dictionary`, `HashSet`을 사용한 경우 작업 보고 시 선택한 이유를 함께 설명한다.
- 일반 순회는 `foreach`, 인덱스가 필요하면 `for`, 삭제를 수반하는 역순 순회에는 역방향 `for`를 사용한다.
- LINQ는 사용하지 않고 반복문으로 작성한다.

### 01-11. 상수 및 매직 넘버
- 의미 있는 수치에는 이름을 부여한다.
  - 게임 느낌을 조정하는 값(이동 속도, 감도, 거리 등)은 `[SerializeField]`로 선언해 Inspector에서 조정한다.
  - 코드가 올바르게 동작하기 위한 보정값은 `const`로 선언한다.
  - `[SerializeField]`에 `Header`, `Tooltip`, `Min`, `Range` 등 Inspector 표시·제한 속성을 붙이지 않는다.
- `0`, `1`처럼 자명한 값은 예외로 한다.
- Unity Layer는 숫자 리터럴로 직접 비교하지 않고, `[SerializeField] private LayerMask`로 선언해 Inspector에서 지정한다.
