package com.isle.isleGame.server;

import com.isle.isleGame.member.entity.Member;
import com.isle.isleGame.member.repository.MemberRepository;
import com.isle.isleGame.response.ErrorResponse;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

@Service
public class ServerService {

    private final ServerRepository serverRepository;
    private final MemberRepository memberRepository;

    public ServerService(ServerRepository serverRepository, MemberRepository memberRepository) {
        this.serverRepository = serverRepository;
        this.memberRepository = memberRepository;
    }

    public ResponseEntity<Object> addServer(String username, ServerAddDTO serverAddDTO) {

        Member member = memberRepository.findByUsername(username);
        Server server = serverRepository.findByMember_IdAndServerName(member.getId(), serverAddDTO.getServer_name());

        if(server != null) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST)
                    .body(new ErrorResponse("이미 존재하는 서버 이름입니다."));
        }

        Server newServer = new Server();
        newServer.setMember(member);
        newServer.setServerName(serverAddDTO.getServer_name());
        newServer.setPassword(serverAddDTO.getPassword());
        newServer.setDifficulty(serverAddDTO.getDifficulty());
        newServer.setDate(0);
        newServer.setTime(0);

        serverRepository.save(newServer);

        return ResponseEntity.status(HttpStatus.OK)
                .build();
    }
}
