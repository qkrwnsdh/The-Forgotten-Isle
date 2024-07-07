package com.isle.isleGame.server.repository;

import com.isle.isleGame.server.entity.Server;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ServerRepository extends JpaRepository <Server, Integer> {

    Server findByMember_IdAndServerName(int memberId, String servername);
}
