# Branch 작성 규칙

> Git Branch 이름을 작성할 때의 규칙.
>
> Branch 생성·전환·삭제는 사용자가 직접 수행한다. (`Docs\Claude\WorkflowRules.md` › 2. 개발, 6. 병합, 브랜치 파기)
>
> 해당 문서는 Branch 이름의 형식과 작성 규칙을 정의한다.

## 1. 형식

- `변경유형/작업명` 형식으로 작성한다.
- 이슈번호는 Branch 이름에 포함하지 않는다.
- 예시 :
    ```text
    feat/player-movement
    fix/monster-hit-detection
    docs/claude-setting
    ```

## 2. 변경 유형

- `Docs\Documentation\Github PR Rule.md` › 1. Title 의 변경 유형 중 하나를 사용한다.
- Branch 이름에서는 모두 소문자로 표기한다. (예: `Feat` → `feat`)

## 3. 작업명

- 영어 소문자로 작성하고, 단어 사이는 `-`로 구분한다.
- 공백, 한글, `#` 등 특수문자는 사용하지 않는다.
- Issue Title의 기능 단위를 짧게 표현한다. (3단어 이내 권장)

## 4. 금지 사항

- `main`, `Develop` 브랜치에서 직접 작업하지 않는다. (`CLAUDE.md` › 절대 원칙 1)
- 하나의 Branch에는 하나의 Issue(작업 목적)만 담는다.
