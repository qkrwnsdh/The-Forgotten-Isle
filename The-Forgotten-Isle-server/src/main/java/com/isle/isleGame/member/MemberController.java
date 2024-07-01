package com.isle.isleGame.member;

import jakarta.validation.Valid;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/account")
public class MemberController {

    private final MemberService memberService;

    public MemberController(MemberService memberService) {
        this.memberService = memberService;
    }

    /*
     * 회원 가입
     */
    @PostMapping("/v2/join")
    public ResponseEntity<Object> join (@Valid @RequestBody JoinDTO dto) {
        return memberService.join(dto);
    }
}
