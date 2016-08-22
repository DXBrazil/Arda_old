/// <binding Clean='clean' />
'use strict';

var gulp = require('gulp');
var sass = require('gulp-sass');
var autoprefixer = require('gulp-autoprefixer');
var sourcemaps = require('gulp-sourcemaps');
var browserSync = require('browser-sync');
var useref = require('gulp-useref');
var uglify = require('gulp-uglify');
var gulpIf = require('gulp-if');
var cssnano = require('gulp-cssnano');
var imagemin = require('gulp-imagemin');
var cache = require('gulp-cache');
var del = require('del');
var runSequence = require('run-sequence');


// Cleaning
gulp.task('clean', function () {
    return del.sync('css').then(function (cb) {
        return cache.clearAll(cb);
    });
})

gulp.task('clean:dist', function () {
    return del.sync(['css/**/*']);
});


gulp.task('sass', function () {
    return gulp.src('./wwwroot/Layouts/ModernUI/sass/**/*.scss') // Gets all files ending with .scss in app/scss and children dirs
      .pipe(sass()) // Passes it through a gulp-sass
      .pipe(gulp.dest('./wwwroot/Layouts/ModernUI/css')); // Outputs it in the css folder
})

// Watchers
gulp.task('watch', function () {
    gulp.watch('./wwwroot/Layouts/ModernUI/sass/**/*.scss', ['sass']);
});

gulp.task('default', function (callback) {
    runSequence(['sass', 'watch'],
      callback
    )
});


gulp.task('build', function (callback) {
    runSequence(
      ['sass'],
      callback
    );
    console.log('Hello Zell!');
});

//
//var gulp = require('gulp');
//var sass = require('gulp-sass')
////var    teste = require('gulp-sass');
//paths.sass = paths.webroot + "sass/**/*.scss";
//paths.css = paths.webroot + "css/**/*.css";
////paths.js = paths.webroot + "js/**/*.js";
////paths.minJs = paths.webroot + "js/**/*.min.js";
////paths.minCss = paths.webroot + "css/**/*.min.css";
////paths.concatJsDest = paths.webroot + "js/site.min.js";
////paths.concatCssDest = paths.webroot + "css/site.min.css";
//;
//gulp.task('sass', function () {
//    return gulp.src(paths.sass)
//      .pipe(sass().on('error', sass.logError))
//      .pipe(gulp.dest(paths.css));
//});

//gulp.task('sass:watch', function () {
//    gulp.watch(paths.sass, ['sass']);
//    console.log(paths);
//});