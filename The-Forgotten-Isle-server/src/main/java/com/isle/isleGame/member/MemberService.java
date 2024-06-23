package com.isle.isleGame.member;

import com.isle.isleGame.response.ErrorResponse;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
@Slf4j
public class MemberService {

    private final MemberRepository memberRepository;
    private final PasswordEncoder passwordEncoder;

    public MemberService(MemberRepository memberRepository, PasswordEncoder passwordEncoder) {
        this.memberRepository = memberRepository;
        this.passwordEncoder = passwordEncoder;
    }

    /*
    * 회원가입
    * 이미 존재하는 아이디면 404 전달
    * 성공하면 200 전달
    */
    public ResponseEntity<Object> join(JoinDTO dto) {

        Optional<Member> member = memberRepository.findById(dto.getId());

        if(member.isPresent()) {
            return ResponseEntity.status(HttpStatus.NOT_FOUND)
                    .body(new ErrorResponse("이미 존재하는 아이디입니다."));
        }
        
        Member joinMember = new Member();
        joinMember.setId(dto.id);
        joinMember.setPassword(passwordEncoder.encode(dto.password));

        joinMember = memberRepository.save(joinMember);

        log.info("join complete = {}", joinMember.getId());

        return ResponseEntity.status(HttpStatus.OK)
                .build();
    }
}
