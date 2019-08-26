pipeline {

    agent any

    parameters {
        string(name:"SOLUTION_NAME", defaultValue:"WebApi.sln", description: "Solution Name")
        string(name:"SOLUTION_DLL", defaultValue:"WebApi.dll", description: "Solution Name")

        string(name:"SONAR_PROJECT_NAME", defaultValue:"WebApi", description: "Solution Name")

        string(name:"DOCKER_IMAGE_NAME", defaultValue:"aspcore_web", description: "Docker Image Name")
        string(name:"DOCKER_REGISTRY", defaultValue:"subtleparesh", description: "Docker Registry Name")
        string(name:"DOCKER_TAG", defaultValue:"latest", description: "Docker Image Tag")

        text(name:"TEST_PROJ_PATH", defaultValue:"", description: "Test Project Path To .csproj file")
        string(name:"HOST_PORT_NO", defaultValue:"8989", description: "Host Bind Port Number")
        string(name:"CONTAINER_PORT_NO", defaultValue:"80", description: "Conatainer Port Number")


        booleanParam(name: 'BUILD', defaultValue: false, description: 'Check To Build')
        booleanParam(name: 'SONAR_ANALYSIS', defaultValue: false, description: 'Check To Sonar Analysis')
        booleanParam(name: 'TEST', defaultValue: false, description: 'Check To Test')
        booleanParam(name: 'PUBLISH', defaultValue: false, description: 'Check To Publish')
        booleanParam(name: 'DOCKER_BUILD', defaultValue: false, description: 'Check To DOCKER_BUILD')
        booleanParam(name: 'DOCKER_PUBLISH', defaultValue: false, description: 'Check To DOCKER_HUB_PUBLISH')
    }

    stages {
        stage('BUILD') {
            when {
                expression { params.BUILD}
            }
            steps {
                bat '''dotnet build %SOLUTION_NAME%'''
            }
        }
        stage('TEST') {
            when {
                expression { params.BUILD}
            }
            steps {
                bat '''dotnet test %SOLUTION_NAME%'''
            }
        }
        
        stage('SONAR ANALYSIS') {
             when {
                expression {  params.SONAR_ANALYSIS }
            }
            steps{
                bat """
                        dotnet ${SONAR_MS_TOOL}  begin /k:"%SONAR_PROJECT_NAME%" /d:sonar.host.url=${SONAR_URL}  /d:sonar.login="${SONAR_TOKEN}"
                        dotnet  build
                        dotnet ${SONAR_MS_TOOL} end  /d:sonar.login="${SONAR_TOKEN}"
                    """
            }     
        }

        stage('PUBLISH') {
             when {
                expression {  params.PUBLISH}
            }
            steps {
                bat '''dotnet publish %SOLUTION_NAME% -p:Configuration=release -v:q -o ../artifacts'''
            }
        }

         stage('DOCKER BUILD IMAGE') {
            when {
                  expression { params.DOCKER_BUILD}
            }
             steps{
                bat 'docker build -t %DOCKER_IMAGE_NAME% .'
             }
        }

        stage('DOCKER TAG & PUSH IMAGE') {
            when {
                  expression { params.DOCKER_PUBLISH}
            }
            
            steps {
             script{
                    docker.withRegistry('','docker_creds')
                    {
                         bat ''' 
                                docker tag %DOCKER_IMAGE_NAME%:%DOCKER_TAG% %DOCKER_REGISTRY%/%DOCKER_IMAGE_NAME% 
                                docker push %DOCKER_REGISTRY%/%DOCKER_IMAGE_NAME%:%DOCKER_TAG%
                            '''
                    }
                }
            }
        }
         stage('REMOVE LOCAL DOCKER IMAGE')
        {
            steps{
                    powershell 'docker rmi  %DOCKER_IMAGE_NAME%'
            }
        }
         stage('PULL DOCKER IMAGE')
        {
            steps{
                    powershell 'docker pull %DOCKER_REGISTRY%/%DOCKER_IMAGE_NAME%:%DOCKER_TAG%'
            }
        }
         stage('DEPLOY DOCKER IMAGE'){
            steps{
                bat 'docker run -p %HOST_PORT_NO%:%CONTAINER_PORT_NO% -e SOLUTION_DLL=%SOLUTION_DLL%  %DOCKER_IMAGE_NAME%'
            }
        }
    }
     post{
	     always{
		     deleteDir()
	  	}
	}
  
}