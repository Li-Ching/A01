package com.example.arhomedesign;

import android.os.Bundle;

import androidx.core.view.ViewCompat;
import androidx.core.widget.NestedScrollView;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageButton;
import android.widget.LinearLayout;
import android.widget.TextView;

public class BedFragment extends Fragment {

    LinearLayout bed1, bed2;
    LinearLayout back;

    TextView bed;
    NestedScrollView scrollView;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_bed, container, false);
        bed = view.findViewById(R.id.tvBed);
        scrollView = view.findViewById(R.id.scv);

        bed1 = view.findViewById(R.id.butBed1);
        bed2 = view.findViewById(R.id.butBed2);
        back = view.findViewById(R.id.back);

        bed.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ViewCompat.postOnAnimation(scrollView, new Runnable() {
                    @Override
                    public void run() {
                        scrollView.smoothScrollTo(0,0);
                    }
                });
            }
        });

        bed1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Bed_1Fragment bed1Fragment = new Bed_1Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, bed1Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        bed2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Bed_2Fragment bed2Fragment = new Bed_2Fragment();

                FragmentManager fragmentManager = getParentFragmentManager();
                FragmentTransaction transaction = fragmentManager.beginTransaction();
                transaction.setCustomAnimations(R.anim.slide_up, R.anim.fade_out, R.anim.fade_in, R.anim.slide_down);
                transaction.replace(R.id.FrameLayout, bed2Fragment);
                transaction.addToBackStack(null);
                transaction.commit();
            }
        });

        back.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                FragmentManager fragmentManager = getParentFragmentManager();
                fragmentManager.popBackStack();

            }
        });

        return view;
    }
}