package com.example.arhomedesign;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class SignupActivity extends AppCompatActivity {
    EditText Username, Email, Password, VerPwd;
    Button signUp;
    TextView cancel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_signup);

        getSupportActionBar().hide();

        signUp = findViewById(R.id.signupButton);
        cancel = findViewById(R.id.tvCancel);

        Username = findViewById(R.id.edtUsername);
        Email = findViewById(R.id.edtEmail);
        Password = findViewById(R.id.edtPassword);
        VerPwd = findViewById(R.id.edtVerPwd);

        signUp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String username = Username.getText().toString();
                String email = Email.getText().toString();
                String password = Password.getText().toString();
                String verPwd = VerPwd.getText().toString();

                if(username.length()==0 && email.length()==0){
                    Toast.makeText(getApplicationContext(), "Enter Username and Email!", Toast.LENGTH_SHORT).show();
                }
                else if(password!=verPwd){
                    Toast.makeText(getApplicationContext(), "Invalid Password!", Toast.LENGTH_SHORT).show();
                }
                else{
                    Intent intent = new Intent(SignupActivity.this, LoginActivity.class);
                    startActivity(intent);
                    finish();
                }
            }
        });

        cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(SignupActivity.this, LoginActivity.class);
                startActivity(intent);
                finish();
            }
        });
    }
}