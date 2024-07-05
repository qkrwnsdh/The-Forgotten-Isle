package com.isle.isleGame.server;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface ServerRepository extends JpaRepository <Server, Integer> {

    Server findByMember_IdAndServerName(int memberId, String servername);
}
