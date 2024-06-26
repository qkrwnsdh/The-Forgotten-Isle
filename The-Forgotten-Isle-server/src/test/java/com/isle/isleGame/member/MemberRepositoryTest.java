package com.isle.isleGame.member;

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
        Member member = new Member("qwe","1234");

        Member saveMember = memberRepository.save(member);

        assertEquals(member, saveMember, "테스트 실패");
    }

}