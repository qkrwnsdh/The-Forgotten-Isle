package com.isle.isleGame.member;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Entity @Data
@AllArgsConstructor @NoArgsConstructor
public class Member {

    @Id @Column(name = "member_id")
    private String id;

    @Column(nullable = false)
    private String password;
}
