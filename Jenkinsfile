pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                echo 'Build'
                sh 'dotnet build WebApi.sln'
            }
        }
    }
}