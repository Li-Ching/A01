package com.example.arhomedesign;

import static java.security.AccessController.getContext;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SignupActivity extends AppCompatActivity {
    private EditText Username, Email, Password, VerPwd;
    private Button signUp;
    private TextView cancel;

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
                else if(!password.equals(verPwd)){
                    Toast.makeText(getApplicationContext(), "Invalid Password!", Toast.LENGTH_SHORT).show();
                }
                else{
                    Intent intent = new Intent(SignupActivity.this, LoginActivity.class);
                    startActivity(intent);
                    Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);
                    UserData modal = new UserData(username, email, password);
                    Call<UserData> call = methods.createPost(modal);


                    // on below line we are executing our method.
                    call.enqueue(new Callback<UserData>() {
                        @Override
                        public void onResponse(Call<UserData> call, Response<UserData> response) {
                            // on below line we are setting empty text
                            // to our both edit text.
                            Username.setText("");
                            Email.setText("");
                            Password.setText("");

                            // we are getting response from our body
                            // and passing it to our modal class.
                            UserData responseFromAPI = response.body();
                        }

                        @Override
                        public void onFailure(Call<UserData> call, Throwable t) {
                            // setting text to our text view when
                            // we get error response from API.
                            Log.e("API Response", "Error: " + t.getMessage());
                        }
                    });
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