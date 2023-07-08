package com.example.arhomedesign;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class ForgotPwdActivity extends AppCompatActivity {

    EditText email;
    Button send;
    TextView cancel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_forgot_pwd);

        getSupportActionBar().hide();

        email = findViewById(R.id.edtEmail);
        send = findViewById(R.id.sendButton);
        cancel = findViewById(R.id.tvCancel);

        cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(ForgotPwdActivity.this, LoginActivity.class);
                startActivity(intent);
                finish();
            }
        });
    }
}