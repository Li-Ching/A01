package com.example.arhomedesign;

import android.annotation.SuppressLint;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import android.content.Intent;
import android.os.Bundle;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;


public class ProfileFragment extends Fragment {
    LinearLayout username;
    TextView Name, Email;

    @SuppressLint("MissingInflatedId")
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_profile, container, false);

        username = view.findViewById(R.id.username);
        Name = view.findViewById(R.id.Name);
        Email = view.findViewById(R.id.Email);

        updateProfileInformation();

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

    private void updateProfileInformation() {
        FirebaseUser user = FirebaseAuth.getInstance().getCurrentUser();
        if (user != null) {
            user.reload();
            String displayName = user.getDisplayName();
            String email = user.getEmail();
            ProfileFragment profileFragment = (ProfileFragment) getParentFragmentManager().findFragmentByTag("ProfileFragment");
            if (profileFragment != null) {
                profileFragment.updateUsername(displayName);
            }

            // Check if user's email is verified
            boolean emailVerified = user.isEmailVerified();

            // Update the TextViews with the refreshed information
            Name.setText(displayName);
            Email.setText(email);
        }
    }

    public void updateUsername(String newUsername) {
        if (Name != null) {
            Name.setText(newUsername);
        }
    }


}