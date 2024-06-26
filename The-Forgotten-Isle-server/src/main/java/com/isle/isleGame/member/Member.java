package com.isle.isleGame.member;

import jakarta.persistence.*;
import lombok.Data;

@Entity
@Data
public class Member {

    @Id @Column(name = "member_id")
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(nullable = false)
    private String username;

    @Column(nullable = false)
    private String password;

    private String role;
}
