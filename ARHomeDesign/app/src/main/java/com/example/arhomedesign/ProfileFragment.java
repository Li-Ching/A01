package com.example.arhomedesign;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.os.Bundle;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.google.firebase.auth.FirebaseAuth;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ProfileFragment extends Fragment {
    LinearLayout username, chgPw;
    TextView Name, Email;

    @SuppressLint("MissingInflatedId")
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_profile, container, false);

        username = view.findViewById(R.id.username);
        Name = view.findViewById(R.id.Name);
        Email = view.findViewById(R.id.Email);
        chgPw = view.findViewById(R.id.chgPw);

        Methods methods = RetrofitClient.getRetrofitInstance().create(Methods.class);
        Call<UserData> call = methods.getProfile();
        call.enqueue(new Callback<UserData>() {
            @Override
            public void onResponse(Call<UserData> call,  Response<UserData> response) {
                UserData userData= response.body();
                if (response.isSuccessful() && userData != null) {
                    Log.d("API Response", "Username: " + response);
                    Log.d("API Response", "Username: " + userData.getUsername());
                    Log.d("API Response", "Email: " + userData.getEmail());
                    Name.setText(userData.getUsername());
                    Email.setText(userData.getEmail());
                } else {
                    Toast.makeText(getContext(), "An error has occurred", Toast.LENGTH_LONG).show();
                    Log.e("API Response", "Error: " + response.message());
                }
            }

            @Override
            public void onFailure(@NonNull Call<UserData> call, @NonNull Throwable t) {
                Toast.makeText(getContext(), "onFailure: " + t.getMessage(), Toast.LENGTH_LONG).show();
            }
        });
        username.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ChangeUsernameFragment changeUsernameFragment = new ChangeUsernameFragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, changeUsernameFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        chgPw.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ChangePasswordFragment changePasswordFragment = new ChangePasswordFragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, changePasswordFragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        Button logoutButton = view.findViewById(R.id.logout_button); // 找到登出按鈕

        logoutButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FirebaseAuth.getInstance().signOut(); // Firebase 登出
                Intent intent = new Intent(getContext(), LoginActivity.class);
                intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TASK);
                startActivity(intent); // 返回登入頁面
                getActivity().finish(); // 結束當前活動
            }
        });
        return view;
    }
}