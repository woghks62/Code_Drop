# Commit 작성 규칙

> Git Commit 메시지를 작성할 때의 규칙.
>
> Commit은 사용자가 직접 수행한다. (`Docs\Claude\WorkflowRules.md` › 4. Commit / Push)
>
> 해당 문서는 Commit 메시지의 각 부분에 대한 작성 규칙을 정의한다.

## 1. 제목

- `[변경 유형] 내용` 형식으로 작성한다.
- 변경 유형은 `Docs\Documentation\Github PR Rule.md` › 1. Title 의 변경 유형 중 하나를 사용하며, 첫 글자를 대문자로 표기한다.
- 내용은 50자 이내로, 변경의 핵심을 명사형으로 끝맺고 마침표를 찍지 않는다.
- 변경된 파일을 나열하지 않고, 작업 단위로 표현한다.
- 예시 :
    ```text
    [Chore] 기본 폴더 구조 생성 및 Git Ignore 설정
    [Feat] 플레이어 이동 구현
    ```

## 2. 본문

- 선택 사항이며, 제목만으로 변경 이유를 알기 어려운 경우에만 작성한다.
- 제목과 한 줄을 띄우고, 리스트로 작성한다.
- 무엇을 바꿨는지보다 왜 바꿨는지를 우선 기재한다.

## 3. 관련 Issue

- 관련 Issue가 있으면 본문 마지막 줄에 `Refs #번호`로 기재한다.
- Commit에는 `Closes`를 사용하지 않는다.
    - Issue Close는 `WorkflowRules.md` › 7. Issue Close 절차를 따른다.

## 4. 커밋 단위

- 하나의 Commit에는 하나의 목적만 담는다.
- 목적이 다른 변경(예: 기능 구현과 문서 수정)은 Commit을 나눈다.
- Unity Editor가 자동 갱신한 파일 중 작업과 무관한 변경은 포함하지 않는다.
