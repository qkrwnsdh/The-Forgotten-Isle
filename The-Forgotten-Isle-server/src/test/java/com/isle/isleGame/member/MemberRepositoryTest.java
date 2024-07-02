package com.isle.isleGame.member;

import com.isle.isleGame.member.entity.Member;
import com.isle.isleGame.member.repository.MemberRepository;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import static org.junit.jupiter.api.Assertions.*;

@SpringBootTest
class MemberRepositoryTest {

    @Autowired
    MemberRepository memberRepository;

    @Test
    void join_test() {
        Member member = new Member();
        member.setUsername("qwe");
        member.setPassword("1234");
        member.setIp("103.234.12.23");

        Member saveMember = memberRepository.save(member);

        assertEquals(member, saveMember, "테스트 실패");
    }

}