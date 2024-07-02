package com.isle.isleGame.member;

import com.isle.isleGame.member.dtos.JoinDTO;
import com.isle.isleGame.member.service.MemberService;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import static org.junit.jupiter.api.Assertions.assertEquals;

@SpringBootTest
class MemberServiceTest {

    @Autowired
    MemberService memberService;

    @Test
    void 회원가입_성공() {
        JoinDTO dto = new JoinDTO();
        dto.setUsername("qwe");
        dto.setPassword("1234");
        dto.setIp("192.168.23.14");

        ResponseEntity<Object> entity = memberService.join(dto);

        assertEquals(entity.getStatusCode(), HttpStatus.OK);
    }

    @Test
    void 회원가입_실패() {
        JoinDTO dto = new JoinDTO();
        dto.setUsername("qwe");
        dto.setPassword("1234");
        dto.setIp("192.168.23.14");

        memberService.join(dto);

        ResponseEntity<Object> entity = memberService.join(dto);

        assertEquals(entity.getStatusCode(), HttpStatus.BAD_REQUEST);
    }
}