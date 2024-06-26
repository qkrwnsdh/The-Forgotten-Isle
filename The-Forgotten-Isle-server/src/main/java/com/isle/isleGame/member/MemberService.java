package com.isle.isleGame.member;

import com.isle.isleGame.response.ErrorResponse;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.Optional;

@Service
@Slf4j
public class MemberService {

    private final MemberRepository memberRepository;
    private final BCryptPasswordEncoder bCryptPasswordEncoder;

    public MemberService(MemberRepository memberRepository, BCryptPasswordEncoder bCryptPasswordEncoder) {
        this.memberRepository = memberRepository;
        this.bCryptPasswordEncoder = bCryptPasswordEncoder;
    }

    /*
    * 회원가입
    * 이미 존재하는 아이디면 404 전달
    * 성공하면 200 전달
    */
    public ResponseEntity<Object> join(JoinDTO dto) {

        Member member = memberRepository.findByUsername(dto.getUsername());

        if(member != null) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body(new ErrorResponse("이미 존재하는 아이디입니다."));
        }
        
        Member joinMember = new Member();
        joinMember.setUsername(dto.username);
        joinMember.setPassword(bCryptPasswordEncoder.encode(dto.password));

        joinMember = memberRepository.save(joinMember);

        log.info("join complete = {}", joinMember.getUsername());

        return ResponseEntity.status(HttpStatus.OK)
                .build();
    }
}
